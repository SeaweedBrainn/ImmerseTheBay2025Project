using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public bool GoodWire;

    public GameObject NonCutWireModel;
    public GameObject CutWireModel;

    public bool Snipped = false;

    public UnityEvent OnSnipEvent;
    public UnityEvent OnBadSnipEvent;
    public UnityEvent OnEnterSnipRange;
    public UnityEvent OnExitSnipRange;

    [Header("Cooldown Settings")]
    public float cooldownTime = 0.2f;
    public bool canOnlyTriggerOnce = true;

    AudioSource audioSource;

    Collider WireCollider;

    public Transform grabbable;

    Vector3 startPosition;
    float lastTriggerTime = -1f;

    void Start()
    {
        NonCutWireModel.SetActive(true);
        CutWireModel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        WireCollider = GetComponent<BoxCollider>();
        startPosition = grabbable.transform.position;
    }

    public void SetGoodWire(bool goodWire)
    {
        this.GoodWire = goodWire;
    }

    void Update()
    {
        if (grabbable && grabbable.transform.position != startPosition)
        {
            CutWireWithPinch();
            if (GoodWire)
            {
                Destroy(grabbable.gameObject);
            }
            else
            {
                canOnlyTriggerOnce = false;
                grabbable.transform.position = startPosition;
            }
        }
    }

    void CutWireWithPinch()
    {
        if (Snipped && GoodWire) return;

        if (Time.time - lastTriggerTime < cooldownTime) return;

        lastTriggerTime = Time.time;

        if (GoodWire)
        {
            NonCutWireModel.SetActive(false);
            CutWireModel.SetActive(true);
            Snipped = true;
            if (audioSource && audioSource.clip)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            OnSnipEvent.Invoke();
        }
        else if (!GoodWire && !canOnlyTriggerOnce)
        {
            canOnlyTriggerOnce = true;
            OnBadSnipEvent.Invoke();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("IndexFingerCollider"))
        {
            // Collision detected with IndexFingerCollider
            Debug.Log("Colliding with IndexFingerCollider");
            // Add your collision handling logic here
            OnEnterSnipRange.Invoke();

            WireSnipper.OnEnterSnipRangeEvent(GoodWire);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("IndexFingerCollider"))
        {
            // Continuously colliding with IndexFingerCollider
            // Add your continuous collision handling logic here
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("IndexFingerCollider"))
        {
            // No longer colliding with IndexFingerCollider
            Debug.Log("No longer colliding with IndexFingerCollider");
            OnExitSnipRange.Invoke();
            WireSnipper.OnExitSnipRangeEvent(GoodWire);
        }
    }
}
