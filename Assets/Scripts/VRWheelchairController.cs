using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class VRWheelchairController : MonoBehaviour
{
    [SerializeField] ActionBasedController leftHandController;
    [SerializeField] ActionBasedController rightHandController;

    List<ActionBasedController> activeControllers;

    [SerializeField] BoxCollider leftWheelHandle;
    [SerializeField] BoxCollider rightWheelHandle;
    private bool leftWheelGrabbed = false;
    private bool rightWheelGrabbed = false;

    [SerializeField] float torqueMultiplier = 10f;
    [SerializeField] float forceMultiplier = 10f;

    Rigidbody rb;

    float left = 0;
    float right = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!leftWheelHandle) { leftWheelHandle = transform.Find("LeftWheelHandle").GetComponent<BoxCollider>(); };
        if (!rightWheelHandle) { rightWheelHandle = transform.Find("RightWheelHandle").GetComponent<BoxCollider>(); };

        activeControllers = new List<ActionBasedController> { leftHandController, rightHandController };

        leftHandController.selectAction.action.started += CheckForLeftHandleGrab;
        rightHandController.selectAction.action.started += CheckForRightHandleGrab;
    }

    void FixedUpdate()
    {
        CheckHandleVolumesForControllers();

        if (leftWheelGrabbed || rightWheelGrabbed)
        {
            GetWheelInputs();

            Vector3 torque = new Vector3(0f, ((-left) + right) * torqueMultiplier, 0f);
            Vector3 force = new Vector3(0f, 0f, -(left + right));

            rb.AddRelativeTorque(torque);
            rb.AddRelativeForce(force * forceMultiplier);
        }
    }

    void CheckHandleVolumesForControllers()
    {
        foreach (ActionBasedController controller in activeControllers)
        {
            bool controllerIsInHandle = false;
            Vector3 controllerPos = controller.transform.position;

            if (PointInOrientedBoundingBox(controllerPos, leftWheelHandle))
            {
                controllerIsInHandle = true;
                break;
            }
            PointInOrientedBoundingBox(controllerPos, rightWheelHandle);
                            controllerIsInHandle = true;


        }
    }

    void OnSelectActionStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    void OnSelectActionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    void CheckForLeftHandleGrab(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Check left handle for grab.
        if (PointInOrientedBoundingBox(leftHandController.transform.position, leftWheelHandle))
        {
            QuestDebug.Log("Left handle grabbed with LeftHand.");


            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            foreach (XRNodeState node in nodeStates)
            {
                if (node.nodeType == XRNode.LeftHand)
                {
                    Vector3 velocity;
                    node.TryGetVelocity(out velocity);
                    left = -velocity.z;
                    QuestDebug.Log($"{node.nodeType}'s velocity is {velocity}");
                }
            }
        }
    }

    void CheckForRightHandleGrab(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // Check right handle for grab.
        if (PointInOrientedBoundingBox(rightHandController.transform.position, rightWheelHandle))
        {
            QuestDebug.Log("Right handle grabbed with RightHand.");

            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            foreach (XRNodeState node in nodeStates)
            {
                if (node.nodeType == XRNode.RightHand)
                {
                    Vector3 velocity;
                    node.TryGetVelocity(out velocity);
                    right = -velocity.z;
                    QuestDebug.Log($"{node.nodeType}'s velocity is {velocity}");
                }
            }
        }
    }

    private void GetWheelInputs()
    {

    }

    // Determines if a point lies within an oriented bounding box.
    bool PointInOrientedBoundingBox(Vector3 point, BoxCollider box)
    {
        point = box.transform.InverseTransformPoint(point) - box.center;

        float halfX = (box.size.x * 0.5f);
        float halfY = (box.size.y * 0.5f);
        float halfZ = (box.size.z * 0.5f);

        if (point.x < halfX && point.x > -halfX &&
           point.y < halfY && point.y > -halfY &&
           point.z < halfZ && point.z > -halfZ)
            return true;
        else
            return false;
    }
}
