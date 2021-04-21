using UnityEngine.UI;
using UnityEngine;

public class RigidbodyDebug : MonoBehaviour
{
    Rigidbody rb;
    public Text label;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        label.text = $"{transform.name}: {rb.velocity}";
    }
}
