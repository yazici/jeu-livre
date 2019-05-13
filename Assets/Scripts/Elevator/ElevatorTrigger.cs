using System.Collections;
using Interactions;
using UnityEngine;

namespace Elevator
{
    public class ElevatorTrigger : Switch
    {
        [SerializeField] private GameObject m_Player;
        [SerializeField] private GameObject m_Elevator;
        [SerializeField] private ElevatorDoor m_ElevatorDoor;
        [SerializeField] private AnimationCurve m_AnimationCurve;
        [SerializeField] private ElevatorDoorTrigger m_ElevatorDoorTrigger;
        [SerializeField] private float m_Speed = 0.1f;
        [SerializeField] private Vector3[] m_PosTargets;


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
            AudioManager.m_Instance.PlaySFX("ElevatorInternalButton");
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
            if (m_ElevatorDoor.m_State) return;

            m_ElevatorDoorTrigger.RemoteInteract();
        }

        private IEnumerator WaitForDoorsClosed()
        {
            if (!m_ElevatorDoor.m_State) yield break;

            m_ElevatorDoorTrigger.RemoteInteract();
            yield return null;
            while (m_ElevatorDoorTrigger.m_IsAnimating)
            {
                yield return null;
            }
        }

        private IEnumerator MoveElevator()
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(WaitForDoorsClosed());
            yield return new WaitForSeconds(0.5f);
            AudioManager.m_Instance.PlaySFX("ElevatorDrive");

            float t = 0;
            Vector3 origin = m_State ? m_PosTargets[0] : m_PosTargets[1];
            Vector3 destination = m_State ? m_PosTargets[1] : m_PosTargets[0];
            m_FpController.enabled = false;
            var stopSFX = false;
            while (t <= 1)
            {
                Vector3 localPosition = m_Elevator.transform.localPosition;

                Vector3 currentPos = localPosition;
                float curvePos = m_AnimationCurve.Evaluate(t);
                Vector3 newPos = origin * (1 - curvePos) + destination * curvePos;
                Vector3 delta = newPos - currentPos;

                localPosition = newPos;
                m_Elevator.transform.localPosition = localPosition;
                t += Time.deltaTime * m_Speed;
                m_Player.transform.localPosition += delta;

                if (t >= 0.8f && !stopSFX)
                {
                    AudioManager.m_Instance.StopSFX("ElevatorDrive");
                    stopSFX = true;
                }
                
                yield return null;
            }

            m_Elevator.transform.localPosition = destination;
            m_FpController.enabled = true;
            AudioManager.m_Instance.PlaySFX("ElevatorBell");

            ResetSwitch();
            yield return new WaitForSeconds(1);
            OpenDoors();
        }
    }
}