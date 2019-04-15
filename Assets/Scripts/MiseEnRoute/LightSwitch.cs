using Interactions;
using UnityEngine;

namespace MiseEnRoute
{
    public class LightSwitch : Trigger
    {
        private new void Start()
        {
            base.Start();
            SetLabel("Appuyer sur l'interrupteur");
        }

        protected override void AfterTrigger()
        {
            if (MiseEnRouteManager.m_Instance.m_PuzzleStep == 1)
            {
                MiseEnRouteManager.m_Instance.m_PuzzleStep = 2;
            }
        }
    }
}