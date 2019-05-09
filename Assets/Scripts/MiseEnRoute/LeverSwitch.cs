using Interactions;
using UnityEngine;

namespace MiseEnRoute
{
    public class LeverSwitch : Switch
    {
        private Animator m_Animator;
        private static readonly int Pulled = Animator.StringToHash("pulled");

        private new void Start()
        {
            base.Start();
            m_Animator = GetComponent<Animator>();
            SetLabel("Actionner le levier");
        }

        protected override void BeforeSwitch()
        {
            m_Animator.SetBool(Pulled, true);
            AudioManager.m_Instance.PlaySFX("Lever", gameObject);
            m_CanInteractWith = false;
            StopLooking();
        }
        
        protected override void AfterSwitch()
        {
            if (MiseEnRouteManager.m_Instance.m_PuzzleStep == 1)
            {
                MiseEnRouteManager.m_Instance.m_PuzzleStep = 2;
            }
        }

        public void ResetSwitch()
        {
            if (!m_State) return;
            m_State = false;
            m_Animator.SetBool(Pulled, false);
            AudioManager.m_Instance.PlaySFX("Lever", gameObject);
            m_CanInteractWith = true;
        }
    }
}