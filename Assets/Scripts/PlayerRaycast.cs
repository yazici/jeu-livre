using Interactions;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Player Main camera
    private Camera m_FpCamera;

    private Lookable m_LastLookable;

    private void Start()
    {
        m_FpCamera = GetComponentInChildren<Camera>();
    }

    public void AttemptRaycast(float range)
    {
        RaycastHit target;
        Transform camTransform = m_FpCamera.transform;

        if (Physics.Raycast(camTransform.position, camTransform.forward, out target, range,
            1 << 10))
        {
            var lookable = target.transform.GetComponent<Lookable>();
            var interactive = target.transform.GetComponent<Interactive>();

            if (lookable)
            {
                lookable.Look();
                m_LastLookable = lookable;
            }

            if (interactive && Input.GetButtonDown("Fire1"))
                interactive.Interact();
        }
        else if (m_LastLookable)
        {
            m_LastLookable.StopLooking();
        }
    }
}