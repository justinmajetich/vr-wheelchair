using UnityEngine;

public class WheelAudio : MonoBehaviour
{
    AudioSource m_AudioSource;
    Rigidbody m_Rigidbody;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float localXAngularVelocity = transform.InverseTransformDirection(m_Rigidbody.angularVelocity).x;

        if (localXAngularVelocity > 0.25f || localXAngularVelocity < -0.25f)
        {
            m_AudioSource.pitch = Mathf.InverseLerp(0f, 10f, localXAngularVelocity) + 1f;
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Pause();
        }
    }
}
