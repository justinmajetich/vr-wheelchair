using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelControl : MonoBehaviour
{
    enum WheelOrientation
    {
        Left,
        Right
    }

    [SerializeField] WheelOrientation wheelOrientation;
    Material m_Material;

    bool isHovered = false;

    void Start()
    {
        m_Material = GetComponent<MeshRenderer>().material;
        m_Material.color = new Color(0, 0, 1, 0.5f);
    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActionBasedController>())
        {
            m_Material.color = new Color(0, 1, 0, 0.5f);
            isHovered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ActionBasedController>())
        {
            m_Material.color = new Color(0, 0, 1, 0.5f);
            isHovered = false;
        }
    }

    //override protected void OnHoverEntered(HoverEnterEventArgs eventArgs)
    //{
    //    m_Material.color = new Color(0, 1, 0, 0.5f);
    //}

    //override protected void OnHoverExited(HoverExitEventArgs eventArgs)
    //{
    //    if (!isSelected)
    //    {
    //        m_Material.color = new Color(0, 0, 1, 0.5f);
    //    }
    //}

    //override protected void OnSelectEntered(SelectEnterEventArgs eventArgs)
    //{
    //    QuestDebug.Log($"{eventArgs.interactor.name} has entered selection of {gameObject.name}");
    //}

    //override protected void OnSelectExited(SelectExitEventArgs eventArgs)
    //{
    //    QuestDebug.Log($"{eventArgs.interactor.name} has exited selection of {gameObject.name}");
    //    m_Material.color = new Color(0, 0, 1, 0.5f);
    //}

}
