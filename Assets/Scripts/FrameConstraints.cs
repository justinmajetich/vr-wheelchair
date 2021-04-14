using UnityEngine;

public class FrameConstraints : MonoBehaviour
{
    [SerializeField] Transform wheelBase;
    //[SerializeField] Transform frontWheelBase;

    [SerializeField] Transform cameraOffset;
    Vector3 cameraOffsetToWheelBaseOffset;


    void Start()
    {
        wheelBase = wheelBase ? wheelBase : transform.Find("WheelBase");
        //frontWheelBase = frontWheelBase ? frontWheelBase : GameObject.Find("FrontWheelBase").transform;

        cameraOffset = transform.Find("Camera Offset");
        cameraOffsetToWheelBaseOffset = cameraOffset.position - wheelBase.position;
    }

    void FixedUpdate()
    {
        //Quaternion parentRotation = mainWheelBase.rotation;

        // Lock front wheel base's rotation to the main wheel base.
        //frontWheelBase.rotation = parentRotation;

        cameraOffset.position = wheelBase.position + cameraOffsetToWheelBaseOffset;
        cameraOffset.rotation = wheelBase.rotation;
    }
}
