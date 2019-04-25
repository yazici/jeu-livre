using System.Collections;
using System.Globalization;
using System.Text;
using UnityEngine;
using TMPro;

public class ConsoleInput : MonoBehaviour
{
    public TMP_Text m_TmpText;
    public TMP_Text m_TmpPlaceholder;
    public TMP_InputField m_TmpInputField;

    [SerializeField] private string[] m_Rooms =
        {"CH-MOL-01", "CH-MOL-01b", "CH-MOL-02", "CH-MOL-03", "CH-MOL-04"};

    [SerializeField] private string[] m_Answers =
        {"Adrénaline", "Glycérol", "Nigerose", "Fucosyllactose", "Acide aspartique"};

    private int m_CurrentStep = 0;
    private bool m_Win;

    private void Update()
    {
        // Open Console
        //if (Input.GetKeyDown(KeyCode.C) && !GameManager.m_Instance.m_IsConsoleTyping && !m_Win)
        {
            InitConsole();
        }
    }

    private void InitConsole()
    {
        //GameManager.m_Instance.m_IsConsoleTyping = true;
        m_TmpInputField.text = "";
        m_TmpInputField.ActivateInputField();
        m_TmpInputField.Select();
        m_TmpPlaceholder.text = "Veuillez entrer la molécule de la salle " + m_Rooms[m_CurrentStep];
    }

    private static string RemoveDiacritics(string text)
    {
        string normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    private bool CheckAnswer(string input)
    {
        string normalizedLowerInput = RemoveDiacritics(input).ToLower();
        return normalizedLowerInput == RemoveDiacritics(m_Answers[m_CurrentStep]).ToLower();
    }

    public void OnEndEdit()
    {
        // Canceled before
        if (m_TmpInputField.wasCanceled)
        {
            m_TmpInputField.text = "";
            OnDeselect();
            return;
        }

        string res = m_TmpInputField.text;

        bool success = CheckAnswer(res);

        if (!success)
        {
            m_TmpText.text = ">>> " + res + " --- Erreur !";

            m_TmpInputField.text = "";
            m_TmpInputField.ActivateInputField();
            m_TmpInputField.Select();
        }
        else
        {
            m_TmpText.text = ">>> Analyse… Succès de l'opération, la molécule \"" + m_Answers[m_CurrentStep] +
                             "\" a été identifiée !";
            m_TmpInputField.text = "";
            OnDeselect();

            if (m_CurrentStep < m_Answers.Length - 1) m_CurrentStep++;
            else
            {
                m_Win = true;
                m_TmpPlaceholder.text = "Toutes les molécules de la zone CH-MOL on été identifiées.";
            }
        }
    }

    public void OnDeselect()
    {
        //GameManager.m_Instance.m_IsConsoleTyping = false;
        m_TmpPlaceholder.text = "Presser C pour écrire dans la console";
    }
}