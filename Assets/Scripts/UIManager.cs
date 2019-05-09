using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager m_Instance;

    public MainSettings m_MainSettings;

    public RectTransform m_LabelTextRectTransform;

    [SerializeField] private TMP_Text m_LabelText;

    private IEnumerator m_CurrentLabelTypingCoroutine;
    private bool m_IsLabelTyping;

    [SerializeField] private Animator m_ReticuleAnimator;

    private static readonly int Active = Animator.StringToHash("active");

    public GameObject m_Reticule;
    public GameObject m_PauseCanvas;

    [SerializeField] private Light m_TorchLight;

    private bool m_IsPaused;

    // Singleton initialization
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        SetReticule(false);
        m_LabelText.text = "";
        m_PauseCanvas.SetActive(false);
    }

    private void Update()
    {
        // Pause management
        // TODO: fade in/out
        if (Input.GetButtonDown("Cancel") && m_IsPaused)
        {
            m_IsPaused = false;
            GameManager.HideCursor();
            m_PauseCanvas.SetActive(false);
        }
        else if (Input.GetButtonDown("Pause"))
        {
            m_IsPaused = true;
            GameManager.ShowCursor();
            m_PauseCanvas.SetActive(true);
        }

        if (!m_TorchLight) return;
        if (Input.GetKeyUp(KeyCode.T) && !GameManager.m_Instance.m_CinematicMode)
        {
            bool isEnabled = m_TorchLight.enabled;
            isEnabled = !isEnabled;
            m_TorchLight.enabled = isEnabled;
            AudioManager.m_Instance.PlaySFX(isEnabled ? "TorchlightOn" : "TorchlightOff");
        }
    }

    /// <summary>
    /// Set the reticule state
    /// </summary>
    /// <param name="active">Is the reticule active (player can interact with the item)</param>
    public void SetReticule(bool active = true)
    {
        m_ReticuleAnimator.SetBool(Active, active);
    }

    public string GetCurrentLabelText()
    {
        return m_LabelText.text;
    }

    public void ChangeLabelText(string message)
    {
        if (m_CurrentLabelTypingCoroutine != null) return;

        m_CurrentLabelTypingCoroutine = TypeLabelText(message);
        StartCoroutine(m_CurrentLabelTypingCoroutine);
    }

    public void ResetLabelText()
    {
        if (m_CurrentLabelTypingCoroutine != null)
            StopCoroutine(m_CurrentLabelTypingCoroutine);
        m_CurrentLabelTypingCoroutine = null;
        m_IsLabelTyping = false;
        m_LabelText.text = "";
    }

    private IEnumerator TypeLabelText(string message)
    {
        if (m_IsLabelTyping) yield break;
        m_IsLabelTyping = true;
        AudioManager.m_Instance.PlaySFX("Typing");

        foreach (char letter in message)
        {
            // Stop immediately
            if (!m_IsLabelTyping)
            {
                m_IsLabelTyping = false;
                yield break;
            }

            m_LabelText.text += letter;
            yield return new WaitForSeconds(m_MainSettings.m_LetterDelay);
        }


        m_IsLabelTyping = false;
    }
}