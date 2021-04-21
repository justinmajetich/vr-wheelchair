using UnityEngine;

public class FrameConstraints : MonoBehaviour
{
    [SerializeField] Transform frame;
    //[SerializeField] Transform frontWheelBase;

    [SerializeField] Transform cameraOffset;
    Vector3 cameraOffsetToFrameOffset;


    void Start()
    {
        frame = frame ? frame : transform.Find("Frame");
        //frontWheelBase = frontWheelBase ? frontWheelBase : GameObject.Find("FrontWheelBase").transform;

        cameraOffset = transform.Find("Camera Offset");
        cameraOffsetToFrameOffset = cameraOffset.position - frame.position;
    }

    void FixedUpdate()
    {
        //Quaternion parentRotation = mainWheelBase.rotation;

        // Lock front wheel base's rotation to the main wheel base.
        //frontWheelBase.rotation = parentRotation;

        cameraOffset.position = frame.position + cameraOffsetToFrameOffset;
        cameraOffset.rotation = frame.rotation;
    }
}
