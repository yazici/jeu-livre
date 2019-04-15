using UnityEngine;

namespace Interactions
{
    public class Trigger : Interactive
    {
        [SerializeField] protected bool m_IsInfiniteTrigger;
        [SerializeField] protected int m_NbLimit;

        private int m_NbActivated;

        // [SerializeField]
        // private string soundName = "";

        private bool CanBeActivated()
        {
            if (m_IsInfiniteTrigger) return true;
            return m_NbActivated <= m_NbLimit;
        }

        public override void Interact()
        {
            if (!CanBeActivated()) return;
            if (IsInteracting) return;
            IsInteracting = true;
            BeforeTrigger();
            m_NbActivated++;
            AfterTrigger();
            IsInteracting = false;
            // if (soundName != "")
            // {
            //     AudioManager.instance.PlaySoundEffect(soundName, gameObject);
            // }
        }

        protected virtual void BeforeTrigger()
        {
        }

        protected virtual void AfterTrigger()
        {
        }
    }
}