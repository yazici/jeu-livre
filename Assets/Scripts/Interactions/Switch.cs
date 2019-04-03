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
            m_State = !m_State;

            // if (soundName != "")
            // {
            //     AudioManager.instance.PlaySoundEffect(soundName, gameObject);
            // }
        }
    }
}