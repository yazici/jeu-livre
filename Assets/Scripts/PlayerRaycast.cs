using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private float m_Range;

    // The interactive object
    private RaycastHit m_Target;
    private Interactive m_TargetObject;

    // Player Main camera
    private Camera m_FpCamera;

    // Max range to pick up object
    public float m_RangeArm = 2f;
    public float m_RangeToFoot = 2f;

    private void Start()
    {
        m_FpCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        m_Range = m_FpCamera.transform.localRotation.x > 0.38f ? m_RangeToFoot : m_RangeArm;

        // TODO: inverse the logic and put it in the interactive objects so we don't spam raycasts every time
        // Raycast detects an interactive object
        if (Physics.Raycast(m_FpCamera.transform.position, m_FpCamera.transform.forward, out m_Target, m_Range, 1 << 10))
        {
            m_TargetObject = m_Target.transform.GetComponent<Interactive>();
            m_TargetObject.Look();
            if (Input.GetButtonDown("Fire1"))
                m_TargetObject.Interact();
        }
        else if (m_TargetObject)
        {
            m_TargetObject.StopLooking();
        }
    }
}