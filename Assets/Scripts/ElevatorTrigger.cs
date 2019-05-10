using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class ElevatorTrigger : Switch
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Elevator;
    [SerializeField] private AnimationCurve m_AnimationCurve;
    [SerializeField] private ElevatorDoorSwitch m_ElevatorDoorSwitch;
    [SerializeField] private float m_Speed = 0.1f;
    [SerializeField] private float[] m_Heights;

    private FpController m_FpController;

    private new void Start()
    {
        base.Start();
        m_State = false;
        m_FpController = m_Player.GetComponent<FpController>();
        SetLabel("Aller au X étage");
    }

    protected override void AfterSwitch()
    {
        m_CanInteractWith = false;
        StopLooking();
        StartCoroutine(MoveElevator());
    }

    private void ResetSwitch()
    {
        SetLabel("Aller au X étage");
        m_CanInteractWith = true;
    }

    private void OpenDoors()
    {
        if (m_ElevatorDoorSwitch.GetState()) return;

        m_ElevatorDoorSwitch.Interact();
    }

    private IEnumerator WaitForDoorsClosed()
    {
        if (!m_ElevatorDoorSwitch.GetState()) yield break;

        m_ElevatorDoorSwitch.Interact();
        yield return null;
        while (m_ElevatorDoorSwitch.m_IsAnimating)
        {
            yield return null;
        }
    }

    private IEnumerator MoveElevator()
    {
        yield return StartCoroutine(WaitForDoorsClosed());

        // AudioManager.m_Instance.PlaySFX("Lever"); TODO
        float t = 0;
        float originY = m_State ? m_Heights[0] : m_Heights[1];
        float destinationY = m_State ? m_Heights[1] : m_Heights[0];
        m_FpController.enabled = false;
        while (t <= 1)
        {
            Vector3 localPosition = m_Elevator.transform.localPosition;

            float currentY = localPosition.y;
            float curvePos = m_AnimationCurve.Evaluate(t);
            float newPosY = originY * (1 - curvePos) + destinationY * curvePos;
            float deltaY = newPosY - currentY;

            localPosition = new Vector3(localPosition.x, newPosY, localPosition.z);
            m_Elevator.transform.localPosition = localPosition;
            t += Time.deltaTime * m_Speed;
            m_Player.transform.localPosition += new Vector3(0, deltaY, 0);
            yield return null;
        }

        Vector3 finalPosition = m_Elevator.transform.localPosition;
        finalPosition = new Vector3(finalPosition.x, destinationY, finalPosition.z);
        m_Elevator.transform.localPosition = finalPosition;
        m_FpController.enabled = true;
        // AudioManager.m_Instance.StopSFX("Lever"); TODO

        ResetSwitch();
        OpenDoors();
    }
}