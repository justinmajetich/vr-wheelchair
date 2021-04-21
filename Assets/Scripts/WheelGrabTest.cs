using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelGrabTest : XRBaseInteractable
{
    Rigidbody m_Rigidbody;

    float wheelRadius;

    bool onSlope = false;

    [Range(0, 5f), Tooltip("Distance from wheel collider at which the interaction manager will force the interactor to deselect.")]
    [SerializeField] float autoDeselectDistance = 0.25f;

    public GameObject grabPointPrefab;
    GameObject grabPoint;

    public Text label1;
    public Text label2;


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        wheelRadius = GetComponent<SphereCollider>().radius;
    }

    private void FixedUpdate()
    {
        // TEMPORARY: maybe throw this in a co-routine for optimization's sake?
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            onSlope = hit.normal != Vector3.up;
            label1.text = $"{transform.name} is on slope: {onSlope}";
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs eventArgs)
    {
        base.OnSelectEntered(eventArgs);

        XRBaseInteractor interactor = eventArgs.interactor;

        // Force end selection with this object.
        interactionManager.CancelInteractableSelection(this);

        SpawnGrabPoint(interactor);

        StartCoroutine(BrakeAssist(interactor));
        StartCoroutine(MonitorDetachDistance(interactor));
    }

    void SpawnGrabPoint(XRBaseInteractor interactor)
    {
        if (grabPoint)
        {
            interactionManager.CancelInteractableSelection(grabPoint.GetComponent<XRGrabInteractable>());
        }

        // Instantiate grab handle interactable.
        grabPoint = Instantiate(grabPointPrefab, interactor.transform.position, interactor.transform.rotation);

        // Attach grab handle to wheel using fixed joint.
        grabPoint.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();

        // Force selection between current interactor and this wheel's grab handle.
        interactionManager.ForceSelect(interactor, grabPoint.GetComponent<XRGrabInteractable>());

    }

    IEnumerator MonitorDetachDistance(XRBaseInteractor interactor)
    {
        // Perform couroutine while this wheel has an active grabPoint.
        while (grabPoint)
        {
            if (Vector3.Distance(transform.position, interactor.transform.position) >= wheelRadius + autoDeselectDistance)
            {
                interactionManager.CancelInteractorSelection(interactor);
            }

            yield return null;
        }
    }

    IEnumerator BrakeAssist(XRBaseInteractor interactor)
    {
        ActionBasedController controller = interactor.GetComponent<ActionBasedController>();
        XRNodeVelocitySupplier interactorVelocity = interactor.GetComponent<XRNodeVelocitySupplier>();

        // Perform couroutine while this wheel has an active grabPoint.
        while (grabPoint)
        {
            // The range of forward/backward controller velocity defining a braking state.
            bool interactorIsBraking = interactorVelocity.velocity.z < 0.05f && interactorVelocity.velocity.z > -0.05f;

            if (interactorIsBraking)
            {
                controller.SendHapticImpulse(0.1f, 0.1f);

                m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity.normalized * 25f);

                SpawnGrabPoint(interactor);
            }
            yield return null;
        }
    }
}
