using Interactions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiseEnRoute
{
    public class Keyboard : Trigger
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private Sprite m_SpriteOk;
        
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
                m_SpriteRenderer.sprite = m_SpriteOk;
                // SceneManager.LoadScene("Scenes/PlaytestsV2/Molecules2");
            }
            else
            {
                AudioManager.m_Instance.PlaySFX("ErrorBeep");
            }
        }
    }
}