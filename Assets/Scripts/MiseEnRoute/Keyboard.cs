using Interactions;

namespace MiseEnRoute
{
    public class Keyboard : Trigger
    {
        private new void Start()
        {
            base.Start();
            SetLabel("Réactiver le système");
        }

        protected override void AfterTrigger()
        {
            if (MiseEnRouteManager.m_Instance.m_PuzzleStep == 2)
            {
                MiseEnRouteManager.m_Instance.m_PuzzleStep = 3;
                print("WIN");
            }
        }
    }
}