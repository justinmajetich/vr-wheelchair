using UnityEngine;


public class FrameConstraints : MonoBehaviour
{
    [SerializeField] Transform frame;
    [SerializeField] Transform cameraOffset;
    Vector3 cameraOffsetToFrameOffset;


    void Start()
    {
        frame = frame ? frame : transform.Find("Frame");
        cameraOffset = cameraOffset ? cameraOffset : transform.Find("Camera Offset");

        cameraOffsetToFrameOffset = cameraOffset.position - frame.position;
    }

    void FixedUpdate()
    {
        cameraOffset.position = frame.position + cameraOffsetToFrameOffset;
        cameraOffset.rotation = frame.rotation;
    }
}
