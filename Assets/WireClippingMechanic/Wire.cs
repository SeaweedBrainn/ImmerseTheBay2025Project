using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public bool GoodWire;

    public GameObject NonCutWireModel;
    public GameObject CutWireModel;

    public bool Snipped = false;

    public UnityEvent OnSnipEvent;
    public UnityEvent OnEnterSnipRange;
    public UnityEvent OnExitSnipRange;

    AudioSource audioSource;

    Collider WireCollider;

    public Transform grabbable;

    Vector3 startPosition;

    void Start()
    {
        NonCutWireModel.SetActive(true);
        CutWireModel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        WireCollider = GetComponent<BoxCollider>();
        startPosition = grabbable.transform.position;
    }

    void Update()
    {
        if (grabbable != null && grabbable.transform.position != startPosition)
        {
            CutWireWithPinch();
            Destroy(grabbable.gameObject);
        }
    }

    void CutWireWithPinch()
    {
        if (!Snipped)
        {
            NonCutWireModel.SetActive(false);
            CutWireModel.SetActive(true);
            Snipped = true;
            OnSnipEvent.Invoke();
            WireSnipper.OnSnipEvent();
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
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
        }

        WireSnipper.OnEnterSnipRangeEvent(GoodWire);
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
        if (other.name == "IndexFingerCollider")
        {
            // No longer colliding with IndexFingerCollider
            Debug.Log("No longer colliding with IndexFingerCollider");
            OnExitSnipRange.Invoke();
        }

        WireSnipper.OnExitSnipRangeEvent(GoodWire);
    }
}
