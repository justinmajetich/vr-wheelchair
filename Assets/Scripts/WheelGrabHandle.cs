using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelGrabHandle : XRGrabInteractable
{
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Destroy(gameObject);
    }
}
