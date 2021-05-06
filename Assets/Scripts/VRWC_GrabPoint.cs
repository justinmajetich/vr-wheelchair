using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Provides a disposable grab point to mediate phyiscs interaction with a VRWheelControl.
/// The GrabPoint will destroy it's GameObject when no longer selected.
/// GrabPoint inherits from XRGrabInteractable, part of Unity's XR Interaction Toolkit.
/// </summary>
public class VRWC_GrabPoint : XRGrabInteractable
{
    protected override void Awake()
    {
        base.Awake();

        // Configure interactable defaults.
        movementType = MovementType.VelocityTracking;
        trackRotation = false;
        throwOnDetach = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Destroy grab point object on selection end.
        Destroy(gameObject);
    }
}
