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

    public void AttemptRaycast(Lookable from, float range)
    {
        RaycastHit target;
        Transform camTransform = m_FpCamera.transform;

        if (Physics.Raycast(camTransform.position, camTransform.forward, out target, range,
            1 << 10))
        {
            var lookable = target.transform.GetComponent<Lookable>();

            // Don't do anything if the target isn't the requester of the raycast
            if (from != lookable) return;

            var interactive = target.transform.GetComponent<Interactive>();

            if (lookable && lookable.enabled)
            {
                if (!interactive || interactive.m_CanInteractWith)
                {
                    lookable.Look();
                    m_LastLookable = lookable;
                }
            }

            if (interactive && Input.GetButtonDown("Fire1") && interactive.enabled && interactive.m_CanInteractWith)
                interactive.Interact();
        }
        else if (m_LastLookable)
        {
            m_LastLookable.StopLooking();
        }
    }
}