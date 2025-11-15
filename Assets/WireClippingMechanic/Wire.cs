using UnityEngine;
using OVR;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public GameObject NonCutWireModel;
    public GameObject CutWireModel;

    public OVRHand LeftHand, RightHand;

    public bool Snipped = false;

    public UnityEvent OnSnipEvent;

    AudioSource audioSource;

    bool LeftHandPinching;
    bool RightHandPinching;

    void Start()
    {
        NonCutWireModel.SetActive(true);
        CutWireModel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool prevLeftHandPinching = LeftHandPinching;
        bool prevRightHandPinching = RightHandPinching;

        LeftHandPinching = LeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        RightHandPinching = RightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        bool LeftPinchPressed = LeftHandPinching && !prevLeftHandPinching;
        bool LeftPinchReleased = !LeftHandPinching && prevLeftHandPinching;

        bool RightPinchPressed = RightHandPinching && !prevRightHandPinching;
        bool RightPinchReleased = !RightHandPinching && prevRightHandPinching;
        Vector3 LeftHandPinchPosition = LeftHand.PointerPose.position;
        Vector3 RightHandPinchPosition = RightHand.PointerPose.position;

        if (LeftPinchPressed)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            Debug.Log("LeftHandPinching");
        }
        if (RightPinchPressed)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
            Debug.Log("RightHandPinching");
        }
        if (!Snipped)
        {
            CutWireWithPinch(LeftHandPinchPosition, LeftPinchPressed);
            CutWireWithPinch(RightHandPinchPosition, RightPinchPressed);
        }
    }

    void CutWireWithPinch(Vector3 pinchPosition, bool isPinching)
    {
        if (isPinching && !Snipped)
        {
            if (Vector3.Distance(pinchPosition, transform.position) < .2f)
            {
                NonCutWireModel.SetActive(false);
                CutWireModel.SetActive(true);
                Snipped = true;
                OnSnipEvent.Invoke();
            }
        }
    }
}
