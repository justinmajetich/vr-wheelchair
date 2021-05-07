using UnityEngine;

/// <summary>
/// Animates wheelchair meshes to express the physical movement of the rig.
/// </summary>
public class VRWC_MeshAnimator : MonoBehaviour
{
    public Rigidbody frame;

    public Transform wheelLeft;
    public Transform wheelRight;

    public Rigidbody casterLeftRB;
    public Rigidbody casterRightRB;

    public Transform wheelLeftMesh;
    public Transform wheelRightMesh;

    public Transform forkLeftMesh;
    public Transform forkRightMesh;

    public Transform casterLeftMesh;
    public Transform casterRightMesh;


    void Update()
    {
        if (frame.velocity.magnitude > 0.05f)
        {
            RotateWheels();
            RotateFork();
            RotateCaster();
        }
    }

    void RotateWheels()
    {
        wheelLeftMesh.rotation = wheelLeft.rotation;
        wheelRightMesh.rotation = wheelRight.rotation;
    }

    void RotateFork()
    {
        forkLeftMesh.rotation = Quaternion.Slerp(forkLeftMesh.rotation, Quaternion.LookRotation(frame.velocity.normalized, transform.up), Time.deltaTime * 8f);
        forkRightMesh.rotation = Quaternion.Slerp(forkRightMesh.rotation, Quaternion.LookRotation(-frame.velocity.normalized, transform.up), Time.deltaTime * 8f);
    }

    void RotateCaster()
    {
        casterLeftMesh.Rotate(-Vector3.right, casterLeftRB.angularVelocity.magnitude);
        casterRightMesh.Rotate(Vector3.right, casterRightRB.angularVelocity.magnitude);
    }
}
