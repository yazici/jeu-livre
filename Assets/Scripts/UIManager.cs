using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Mouse move input
    private float m_DeltaMouseX;
    private float m_DeltaMouseY;

    [SerializeField] private float m_SensitivityX = 1;
    [SerializeField] private float m_SensitivityY = 1;

    [SerializeField] private float m_SpeedUIRecenterX = 0.1f;
    [SerializeField] private float m_SpeedUIRecenterY = 0.01f;

    [SerializeField] private RectTransform m_RobotUI;

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
        StartCoroutine(RecenterUI());
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
        else if (Input.GetButtonDown("Pause") && SceneManager.GetActiveScene().name != "StartingConsole")
        {
            m_IsPaused = true;
            GameManager.ShowCursor();
            m_PauseCanvas.SetActive(true);
        }

        if (m_TorchLight)
        {
            if (Input.GetKeyUp(KeyCode.F) && !GameManager.m_Instance.m_CinematicMode)
            {
                bool isEnabled = m_TorchLight.enabled;
                isEnabled = !isEnabled;
                m_TorchLight.enabled = isEnabled;
                AudioManager.m_Instance.PlaySFX(isEnabled ? "TorchlightOn" : "TorchlightOff");
            }
        }

        // ------------------------------ //
        // -------- Delay for UI -------- //
        // ------------------------------ //
        if (
            Cursor.lockState == CursorLockMode.Locked &&
            !GameManager.m_Instance.m_CinematicMode
        )
        {
            float mouseX = Input.GetAxis("Mouse X") * m_SensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * m_SensitivityY;
            m_DeltaMouseX += mouseX;
            m_DeltaMouseY += mouseY;

            m_RobotUI.offsetMin = new Vector2(-m_DeltaMouseX, -m_DeltaMouseY);
            m_RobotUI.offsetMax = new Vector2(-m_DeltaMouseX, -m_DeltaMouseY);   
        }
    }

    private IEnumerator RecenterUI()
    {
        float tX = 0;
        float tY = 0;
        while (true)
        {
            if (
                Cursor.lockState == CursorLockMode.Locked &&
                !GameManager.m_Instance.m_CinematicMode &&
                m_RobotUI.offsetMin != new Vector2(0, 0)
            )
            {
                m_DeltaMouseX = Mathf.Lerp(m_DeltaMouseX, 0, tX);
                m_DeltaMouseY = Mathf.Lerp(m_DeltaMouseY, 0, tY);

                tX += Time.deltaTime * m_SpeedUIRecenterX;
                tY += Time.deltaTime * m_SpeedUIRecenterY;
            }
            else
            {
                tX = 0;
                tY = 0;
            }

            yield return null;
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