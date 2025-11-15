using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public GameObject NonCutWireModel;
    public GameObject CutWireModel;

    public bool Snipped = false;

    public UnityEvent OnSnipEvent;

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
        if (grabbable.transform.position != startPosition)
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
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }
}
