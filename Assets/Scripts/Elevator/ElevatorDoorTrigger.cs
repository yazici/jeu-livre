using System.Collections;
using Interactions;
using UnityEngine;

namespace Elevator
{
    public class ElevatorDoorTrigger : Trigger
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private ElevatorDoor m_ElevatorDoor;
        private static readonly int ToggleDoors = Animator.StringToHash("ToggleDoors");
        public bool m_IsAnimating;

        private new void Start()
        {
            base.Start();
            m_ElevatorDoor.m_State = false;
            SetLabel("Ouvrir les portes de l'ascenseur");
        }

        protected override void BeforeTrigger()
        {
            AudioManager.m_Instance.PlaySFX("ElevatorExternalCallButton");
        }

        protected override void AfterTrigger()
        {
            m_ElevatorDoor.m_State = !m_ElevatorDoor.m_State;
            m_CanInteractWith = false;
            StopLooking();
            m_Animator.SetTrigger(ToggleDoors);
            m_IsAnimating = true;
            StartCoroutine(ResetSwitchAfterAnim());
        }

        private IEnumerator ResetSwitchAfterAnim()
        {
            yield return StartCoroutine(WaitForAnim());
            SetLabel((m_ElevatorDoor.m_State ? "Fermer" : "Ouvrir") + " les portes de l'ascenseur");
            m_CanInteractWith = true;
            m_IsAnimating = false;
        }


        private IEnumerator WaitForAnim()
        {
            yield return null;
            while (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1 || m_Animator.IsInTransition(0))
            {
                yield return null;
            }
        }

        public void RemoteInteract()
        {
            AfterTrigger();
        }
    }
}