using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiseEnRoute
{
    public class StartingConsole : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ConsoleText;
        [SerializeField] private Slider m_LoadingBarSlider;
        [SerializeField] private Animation m_LoadingBarAnimation;
        [SerializeField] private TMP_InputField m_InputField;
        [SerializeField] private ScrollRect m_ScrollRect;

        private string m_IdName;
        private string m_Password;

        private bool m_IsUserValide;
        private AsyncOperation m_AsyncLoad;
        [SerializeField] private bool m_SceneIsReady;

        private void Start()
        {
            m_InputField.Select();
            m_InputField.ActivateInputField();
            m_LoadingBarSlider.gameObject.SetActive(false);

            m_IdName = GameManager.m_Instance.m_MainSettings.m_IdName;
            m_Password = GameManager.m_Instance.m_MainSettings.m_Password;
        }

        public void DisplayConsoleText()
        {
            // Username Step
            if (!m_IsUserValide)
            {
                if (m_InputField.text == m_IdName)
                {
                    m_ConsoleText.text = string.Format("{0} {1}\nIdentifiant valide\n\nMot de passe :",
                        m_ConsoleText.text, m_InputField.text);
                    CleanInputField();
                    m_IsUserValide = true;
                    StartCoroutine(WaitAsyncLoad());
                }
                else
                {
                    m_ConsoleText.text = string.Format("{0} {1}\n\nUtilisateur non reconnu.\n\nIdentifiant :",
                        m_ConsoleText.text, m_InputField.text);
                    CleanInputField();
                }
            }
            // Password Step
            else
            {
                if (m_InputField.text == m_Password)
                {
                    m_ConsoleText.text = string.Format("{0}\n\nUtilisateur connecté.", m_ConsoleText.text);
                    CleanInputField();
                    m_LoadingBarSlider.gameObject.SetActive(true);
                    m_InputField.enabled = false;
                    StartCoroutine(PlayLoadBar());
                }
                else
                {
                    m_ConsoleText.text = string.Format("{0}\n\nERREUR.\nMot de passe :", m_ConsoleText.text);
                    CleanInputField();
                }
            }
            
            m_ScrollRect.verticalNormalizedPosition = 0f;
        }

        private IEnumerator WaitAsyncLoad()
        {
            m_AsyncLoad = SceneManager.LoadSceneAsync(GameManager.m_Instance.m_MainSettings.m_MainScene);
            // Don't let the Scene activate until we allow it to
            m_AsyncLoad.allowSceneActivation = false;

            // Wait until the asynchronous scene fully loads
            while (!m_AsyncLoad.isDone)
            {
                if (m_AsyncLoad.progress >= 0.9f)
                {
                    m_SceneIsReady = true;
                }

                yield return null;
            }
        }

        private IEnumerator PlayLoadBar()
        {
            m_LoadingBarSlider.value = 0;

            // Wait until the asynchronous scene fully loads
            while (!m_SceneIsReady)
            {
                yield return null;
            }

            m_LoadingBarAnimation.Play();
            yield return WaitForAnimation();

            m_AsyncLoad.allowSceneActivation = true;
        }

        private void CleanInputField()
        {
            m_InputField.text = "";
            m_InputField.Select();
            m_InputField.ActivateInputField();
        }

        private IEnumerator WaitForAnimation()
        {
            do
            {
                yield return null;
            } while (m_LoadingBarAnimation.isPlaying);
        }
    }
}