using UnityEngine;

namespace Interactions
{
    public class Switch : Interactive
    {
        [SerializeField] protected bool m_State;

        // [SerializeField]
        // private string soundName = "";

        public override void Interact()
        {
            if (IsInteracting) return;
            IsInteracting = true;
            BeforeSwitch();
            m_State = !m_State;
            AfterSwitch();
            IsInteracting = false;
            // if (soundName != "")
            // {
            //     AudioManager.instance.PlaySoundEffect(soundName, gameObject);
            // }
        }

        protected virtual void BeforeSwitch()
        {
        }

        protected virtual void AfterSwitch()
        {
        }

        protected bool IsActivated()
        {
            return m_State;
        }
    }
}