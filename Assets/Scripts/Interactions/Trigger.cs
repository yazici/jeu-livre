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

        protected bool CanBeActivated()
        {
            if (m_IsInfiniteTrigger) return true;
            return m_NbActivated <= m_NbLimit;
        }

        public override void Interact()
        {
            m_NbActivated++;
            // if (soundName != "")
            // {
            //     AudioManager.instance.PlaySoundEffect(soundName, gameObject);
            // }
        }
    }
}