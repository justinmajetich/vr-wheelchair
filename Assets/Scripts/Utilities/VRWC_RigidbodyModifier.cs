using UnityEngine;

/// <summary>
/// Utility class exposing otherwise hidden Rigidbody properties for in-editor modification.
/// </summary>
public class VRWC_RigidbodyModifier : MonoBehaviour
{
    Rigidbody rb;

    [Header("Max Angular Veloctiy")]
    [SerializeField, Range(0, 100), Tooltip("Upward bounds of a Rigidbody's angular velocity. Unity default is 7.")]
    float maxAngularVelocity = 7f;

    [Header("Center of Mass")]
    [SerializeField, Tooltip("If unchecked, center of mass will be calculated automatically. Once a custom center is set, it will no longer be recomputed automatically.")]
    bool useCustomCenterOfMass = false;

    [SerializeField, ConditionalHide("useCustomCenterOfMass", true)]
    Vector3 centerOfMass;

    [SerializeField]
    bool visualizeCenterOfMass = false;
    GameObject visualization = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.maxAngularVelocity = maxAngularVelocity;

        if (useCustomCenterOfMass)
        {
            rb.centerOfMass = centerOfMass;
        }

        if (visualizeCenterOfMass)
        {
            visualization = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visualization.GetComponent<Collider>().enabled = false;
            visualization.transform.position = rb.worldCenterOfMass;
            visualization.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            visualization.GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
    }

    void Update()
    {
        if (useCustomCenterOfMass)
        {
            rb.centerOfMass = centerOfMass;
        }
        else
        {
            rb.ResetCenterOfMass();
        }

        if (!visualizeCenterOfMass && visualization)
        {
            Destroy(visualization);
            visualization = null;
        }

        if (visualizeCenterOfMass && !visualization)
        {
            visualization = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visualization.GetComponent<Collider>().enabled = false;
            visualization.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            visualization.GetComponent<MeshRenderer>().material.color = Color.magenta;
        }

        if (visualization)
        {
            visualization.transform.position = rb.worldCenterOfMass;
        }
    }
}
