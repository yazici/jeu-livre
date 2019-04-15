namespace Interactions
{
    public abstract class Interactive : Lookable
    {
        protected bool IsInteracting;

        public abstract void Interact();
    }
}