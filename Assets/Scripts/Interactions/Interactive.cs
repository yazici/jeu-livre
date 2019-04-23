using UnityEngine;

namespace Interactions
{
    public abstract class Interactive : Lookable
    {
        [HideInInspector] public bool m_CanInteractWith = true;
        protected bool IsInteracting;

        public abstract void Interact();
    }
}