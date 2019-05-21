using System.Collections;
using UnityEngine;

namespace Elevator
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private GameObject m_Player;
        [SerializeField] private GameObject m_Elevator;
        [SerializeField] private ElevatorDoor m_ElevatorDoor;
        [SerializeField] private AnimationCurve m_AnimationCurve;
        [SerializeField] private ElevatorDoorTrigger m_ElevatorDoorTrigger;
        [SerializeField] private float m_Speed = 0.1f;
        [SerializeField] private Vector3[] m_PosTargets;

        private FpController m_FpController;

        private bool m_State = true;

        private new void Start()
        {
            m_FpController = m_Player.GetComponent<FpController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            AudioManager.m_Instance.PlaySFX("ElevatorInternalButton");
            AudioManager.m_Instance.StopSFX("DroneMotor");
            StartCoroutine(MoveElevator());
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
            m_FpController.enabled = false;
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(WaitForDoorsClosed());
            yield return new WaitForSeconds(0.5f);
            AudioManager.m_Instance.PlaySFX("ElevatorDrive");

            float t = 0;
            Vector3 origin = m_State ? m_PosTargets[0] : m_PosTargets[1];
            Vector3 destination = m_State ? m_PosTargets[1] : m_PosTargets[0];
            var stopSFX = false;
            while (t <= 1)
            {
                Vector3 localPosition = m_Elevator.transform.localPosition;

                Vector3 currentPos = localPosition;
                float curvePos = m_AnimationCurve.Evaluate(t);
                Vector3 newPos = origin * (1 - curvePos) + destination * curvePos;
                Vector3 delta = newPos - currentPos;

                print("newPos: " + newPos);
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

            yield return new WaitForSeconds(1);
            OpenDoors();
        }
    }
}