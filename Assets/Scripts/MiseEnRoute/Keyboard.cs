using Interactions;
using UnityEngine.SceneManagement;

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
            // Win
            if (MiseEnRouteManager.m_Instance.m_PuzzleStep == 2)
            {
                MiseEnRouteManager.m_Instance.m_PuzzleStep = 3;
                AudioManager.m_Instance.PlaySFX("ValidationBeep");
                MiseEnRouteManager.m_Instance.MiseEnRouteSuccessful();
                // SceneManager.LoadScene("Scenes/PlaytestsV2/Molecules2");
            }
            else
            {
                AudioManager.m_Instance.PlaySFX("ErrorBeep");
            }
        }
    }
}