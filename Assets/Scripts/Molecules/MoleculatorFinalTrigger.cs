using System.Collections;
using Interactions;
using UnityEngine;
using UnityEngine.UI;

namespace Molecules
{
    public class MoleculatorFinalTrigger : Trigger
    {
        [SerializeField] private ShakeableTransform m_ShakeableTransform;
        [SerializeField] private GameObject m_CutoffCanvas;
        [SerializeField] private GameObject m_Player;
        [SerializeField] private Vector3 m_PlayerDestination;

        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private Sprite m_SpriteFinal;

        [SerializeField] private GameObject m_EdificeFragment;
        
        private Image m_BlackScreen;

        private new void Start()
        {
            base.Start();
            m_BlackScreen = m_CutoffCanvas.GetComponentInChildren<Image>();
        }

        protected override void BeforeTrigger()
        {
            // Win
            if (MoleculesManager.m_Instance.m_SyntheseValide)
            {
                m_SpriteRenderer.sprite = m_SpriteFinal;
                AudioManager.m_Instance.PlaySFX("ValidationBeep");
                ShakeableTransform.OnEarthshakeEnded += CutToEnd;
                m_ShakeableTransform.enabled = true;
                m_ShakeableTransform.AddTrauma(0.5f);
                StopLooking();
                m_CanInteractWith = false;
                AudioManager.m_Instance.PlaySFX("Earthquake");
            }
            else
            {
                AudioManager.m_Instance.PlaySFX("ErrorBeep");
            }
        }

        private void CutToEnd()
        {
            AudioManager.m_Instance.StopSFX("Earthquake");
            m_EdificeFragment.SetActive(true);
            StartCoroutine(WaitThenTeleportPlayer());
        }

        private IEnumerator WaitThenTeleportPlayer()
        {
            yield return new WaitForSeconds(1);
            GameManager.m_Instance.m_CinematicMode = true;
            yield return StartCoroutine(FadeOut());
            TeleportPlayer();
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(FadeIn());
            GameManager.m_Instance.m_CinematicMode = false;
        }

        private IEnumerator FadeOut()
        {
            m_CutoffCanvas.SetActive(true);
            float t = 0;
            while (t <= 1)
            {
                m_BlackScreen.color =
                    Color.Lerp(Color.clear, new Color(0, 0, 0, 1), t);
                t += Time.deltaTime * 1;
                yield return null;
            }

            m_BlackScreen.color = Color.black;
        }

        private IEnumerator FadeIn()
        {
            float t = 0;
            while (t <= 1)
            {
                m_BlackScreen.color =
                    Color.Lerp(new Color(0, 0, 0, 1), Color.clear, t);
                t += Time.deltaTime * 1;
                yield return null;
            }

            m_BlackScreen.color = Color.clear;
            m_CutoffCanvas.SetActive(false);
        }

        private void TeleportPlayer()
        {
            m_Player.transform.localPosition = m_PlayerDestination;
        }
    }
}