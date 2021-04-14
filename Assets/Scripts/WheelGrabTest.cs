using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelGrabTest : XRBaseInteractable
{
    Material initialMat;
    public Material hoveredMat;

    public GameObject grabHandlePrefab;
    GameObject grabHandle;


    private void Start()
    {
        initialMat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (isSelected || isHovered)
        {
            GetComponent<MeshRenderer>().material = hoveredMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = initialMat;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs eventArgs)
    {
        base.OnSelectEntered(eventArgs);

        XRBaseInteractor interactor = eventArgs.interactor;

        Vector3 interactPos = interactor.transform.position;
        Quaternion interactRot = interactor.transform.rotation;

        // Force end selection with this object.
        interactionManager.CancelInteractableSelection(this);

        // Instantiate grab handle interactable.
        grabHandle = Instantiate(grabHandlePrefab, interactPos, interactRot);

        // Attach grab handle to wheel using fixed joint.
        grabHandle.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();

        // Force selection between current interactor and this wheel's grab handle.
        interactionManager.ForceSelect(interactor, grabHandle.GetComponent<XRGrabInteractable>());
    }
}
