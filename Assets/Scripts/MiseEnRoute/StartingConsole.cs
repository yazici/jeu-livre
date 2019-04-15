using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiseEnRoute
{
    public class StartingConsole : MonoBehaviour
    {
        public Text m_ConsoleText;
        private InputField m_InputField;

        [SerializeField] private string m_IdName = "aurorechamrouge";
        [SerializeField] private string m_Password = "%SRghatN895";

        private bool m_IsUserValide;

        private void Start()
        {
            m_InputField = GetComponent<InputField>();
            m_InputField.Select();
            m_InputField.ActivateInputField();
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
                }
                else
                {
                    m_ConsoleText.text = string.Format("{0} {1}\n\nUtilisateur non reconnu.\n\nIdentifiant : ",
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
                    
                    // TODO: loading

                    SceneManager.LoadScene("Mise en route");
                }
                else
                {
                    m_ConsoleText.text = string.Format("{0}\n\nERREUR.\nMot de passe :", m_ConsoleText.text);
                    CleanInputField();
                }
            }
        }

        private void CleanInputField()
        {
            m_InputField.text = "";
            m_InputField.Select();
            m_InputField.ActivateInputField();
        }
    }
}