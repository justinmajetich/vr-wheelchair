using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Supplies the current velocity of a specific XRNode. This class is meant to supplement the ActionBasedController by providing a XR Controller with velocity input.
/// </summary>
public class VRWC_XRNodeVelocitySupplier : MonoBehaviour
{
    [SerializeField, Tooltip("The XRNode for which velocity should be tracked. This should be LeftHand or RightHand")]
    XRNode trackedNode;

    Vector3 _velocity = Vector3.zero;

    /// <summary>
    /// Most recently tracked velocity of attached transform. Read only.;
    /// </summary>
    public Vector3 velocity { get => _velocity; }

    private void Start()
    {
        InputDevices.GetDeviceAtXRNode(trackedNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out _velocity);
    }

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(trackedNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out _velocity);
    }
}
