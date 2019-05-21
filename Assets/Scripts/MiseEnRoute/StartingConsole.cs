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
        [SerializeField] private TMP_Text m_TypedText;
        [SerializeField] private Slider m_LoadingBarSlider;

        [SerializeField] private Animation m_LoadingBarAnimation;

        //[SerializeField] private TMP_InputField m_InputField;
        [SerializeField] private ScrollRect m_ScrollRect;

        [SerializeField] private GameObject m_ReticuleCanvas;

        private string m_IdName;
        private string m_Password;

        private bool m_IsUserValide;
        private AsyncOperation m_AsyncLoad;
        [SerializeField] private bool m_SceneIsReady;

        private bool m_IsBlinking = true;
        private string typedText;

        private int m_ConsoleTextMinLength;

        private bool m_CanType;

        private void Start()
        {
            m_ReticuleCanvas.SetActive(false);
            
            //m_InputField.Select();
            //m_InputField.ActivateInputField();
            m_LoadingBarSlider.gameObject.SetActive(false);

            //m_InputField.text = "Lancement du Système...\nVersion 2145.23.54\n\nProtocole de sécurité actif.\n\nIdentifiant:";

            m_IdName = GameManager.m_Instance.m_MainSettings.m_IdName;
            m_Password = GameManager.m_Instance.m_MainSettings.m_Password;


            string consoleText = m_ConsoleText.text;
            m_ConsoleTextMinLength = consoleText.Length;
            m_ConsoleText.text = "";
            StartCoroutine(TypeLabelText(consoleText));
        }

        private void Update()
        {
            if (!m_CanType) return;
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (m_TypedText.text.Length == 0) return;
                    if (m_ConsoleText.text.Length <= m_ConsoleTextMinLength) return;
                    if (m_ConsoleText.text.Length == m_ConsoleTextMinLength + 1 &&
                        m_ConsoleText.text.EndsWith("|")) return;

                    if (m_ConsoleText.text.EndsWith("|"))
                    {
                        m_ConsoleText.text = m_ConsoleText.text.Substring(0, m_ConsoleText.text.Length - 1);
                        m_TypedText.text = m_TypedText.text.Substring(0, m_TypedText.text.Length - 1);
                    }

                    m_ConsoleText.text = m_ConsoleText.text.Substring(0, m_ConsoleText.text.Length - 1);
                    m_TypedText.text = m_TypedText.text.Substring(0, m_TypedText.text.Length - 1);
                }
                else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    if (m_ConsoleText.text.EndsWith("|"))
                    {
                        m_ConsoleText.text = m_ConsoleText.text.Substring(0, m_ConsoleText.text.Length - 1);
                        m_TypedText.text = m_TypedText.text.Substring(0, m_TypedText.text.Length - 1);
                    }

                    CheckText(m_TypedText.text);
                    m_TypedText.text = "";
                    //print(typedText);
                    //typedText = "";
                }
                else
                {
                    if (m_ConsoleText.text.EndsWith("|"))
                    {
                        m_ConsoleText.text = m_ConsoleText.text.Substring(0, m_ConsoleText.text.Length - 1);
                        m_TypedText.text = m_TypedText.text.Substring(0, m_TypedText.text.Length - 1);
                    }

                    m_ConsoleText.text += c;
                    m_TypedText.text += c;
                }
            }
        }

        private IEnumerator BlinkCaret()
        {
            while (m_IsBlinking)
            {
                yield return new WaitForEndOfFrame();
                if (!m_ConsoleText.text.EndsWith("|"))
                {
                    m_ConsoleText.text += "|";
                    m_TypedText.text += "|";
                }
                else
                {
                    m_ConsoleText.text = m_ConsoleText.text.Substring(0, m_ConsoleText.text.Length - 1);
                    m_TypedText.text = m_TypedText.text.Substring(0, m_TypedText.text.Length - 1);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }


        public void CheckText(string typedText)
        {
            // Username Step
            if (!m_IsUserValide)
            {
                if (typedText == m_IdName)
                {
                    m_ConsoleText.text += "\n\nIdentifiant valide\n\nMot de passe : ";
                    m_IsUserValide = true;
                    m_ScrollRect.verticalNormalizedPosition = 0f;
                }
                else
                {
                    m_ConsoleText.text += "\n\nUtilisateur non reconnu.\n\nIdentifiant : ";
                }
            }
            // Password Step
            else
            {
                if (typedText == m_Password)
                {
                    m_ConsoleText.text += "\n\nUtilisateur connecté.";
                    //CleanInputField();
                    m_LoadingBarSlider.gameObject.SetActive(true);
                    //m_InputField.enabled = false;
                    StartCoroutine(PlayLoadBar());
                }
                else
                {
                    m_ConsoleText.text += "\n\nERREUR.\n\nMot de passe : ";
                    m_ScrollRect.verticalNormalizedPosition = 0f;
                    //CleanInputField();
                }
            }

            m_ConsoleTextMinLength = m_ConsoleText.text.Length;

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

            m_ReticuleCanvas.SetActive(true);
            m_AsyncLoad.allowSceneActivation = true;
        }

        private IEnumerator WaitForAnimation()
        {
            do
            {
                yield return null;
            } while (m_LoadingBarAnimation.isPlaying);
        }

        private IEnumerator TypeLabelText(string message)
        {
            yield return null;

            foreach (char letter in message)
            {
                m_ConsoleText.text += letter;
                yield return new WaitForSeconds(0.015f);
            }

            yield return new WaitForSeconds(0.015f);
            m_CanType = true;
            StartCoroutine(WaitAsyncLoad());
            StartCoroutine(BlinkCaret());
        }
    }
}