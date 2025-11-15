using UnityEngine;
using OVR;

public class Wire : MonoBehaviour
{
    public GameObject NonCutWireModel;
    public GameObject CutWireModel;

    public OVRHand LeftHand, RightHand;

    public Transform LeftPinchSphere, RightPinchSphere;

    void Start()
    {

    }

    void Update()
    {
        bool LeftHandPinching = LeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool RightHandPinching = RightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        Vector3 LeftHandPinchPosition = LeftHand.PointerPose.position;
        Vector3 RightHandPinchPosition = RightHand.PointerPose.position;

        LeftPinchSphere.position = LeftHandPinchPosition;
        RightPinchSphere.position = RightHandPinchPosition;

        if (LeftHandPinching)
        {
            Debug.Log("LeftHandPinching");
        }
        if (RightHandPinching)
        {
            Debug.Log("RightHandPinching");
        }

        CutWireWithPinch(LeftHandPinchPosition, LeftHandPinching);
        CutWireWithPinch(RightHandPinchPosition, RightHandPinching);
    }

    void CutWireWithPinch(Vector3 pinchPosition, bool isPinching)
    {
        if (isPinching)
        {
            if (Vector3.Distance(pinchPosition, transform.position) < .2f)
            {
                NonCutWireModel.SetActive(false);
                CutWireModel.SetActive(true);
            }
        }
    }
}
