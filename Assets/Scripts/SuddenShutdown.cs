using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SuddenShutdown : MonoBehaviour
{
    private ColorGrading m_ColorGradingLayer;
    private Animation m_Animation;

    [SerializeField] private PixelBoy m_PixelBoy;
    [SerializeField] private GlitchEffect m_GlitchEffect;

    [SerializeField] private float m_AnimSpeed = 1;

    [SerializeField] private GameObject m_ShutdownCanvas;
    [SerializeField] private TMP_Text m_TMPShutdownText;

    private void Awake()
    {
        var volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out m_ColorGradingLayer);
        m_Animation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CloseSystem();
        }
    }

    public void CloseSystem()
    {
        m_PixelBoy.enabled = true;
        m_GlitchEffect.enabled = true;
        m_ShutdownCanvas.SetActive(true);
        GameManager.m_Instance.m_CinematicMode = true;
        m_Animation.Play("DroneShutdown");
        StartCoroutine(TypeShutdownText());
        m_ColorGradingLayer.saturation.value = 0f;
        StartCoroutine(PlayColorAnim());
    }

    private IEnumerator PlayColorAnim()
    {
        var t = 0f;
        while (t <= 1)
        {
            float v = Mathf.Lerp(0, -100, t);
            m_ColorGradingLayer.saturation.value = v;
            t += 0.5f * Time.deltaTime * m_AnimSpeed;
            yield return null;
        }

        m_ColorGradingLayer.saturation.value = -100f;
    }

    private IEnumerator TypeShutdownText()
    {
        AudioManager.m_Instance.PlaySFX("CommunicationNoise");
        string text = m_TMPShutdownText.text;
        m_TMPShutdownText.text = "";
        yield return null;
        var lastChar = '\n';
        
        AudioManager.m_Instance.PlaySFX("ErrorTyping");

        foreach (char letter in text)
        {
            if (letter == '\n')
            {
                AudioManager.m_Instance.StopSFX("ErrorTyping");
                yield return new WaitForSeconds(0.5f);
            }

            if (letter != '\n' && lastChar == '\n')
            {
                AudioManager.m_Instance.PlaySFX("ErrorTyping");
            }

            m_TMPShutdownText.text += letter;
            lastChar = letter;
            yield return new WaitForSeconds(GameManager.m_Instance.m_MainSettings.m_LetterDelay);
        }
        
        AudioManager.m_Instance.StopSFX("ErrorTyping");
        var countdown = 4;
        do
        {
            yield return new WaitForSeconds(1);
            m_TMPShutdownText.text =
                m_TMPShutdownText.text.Substring(0, m_TMPShutdownText.text.Length - 1) + countdown;
            countdown--;
        } while (countdown >= 0);
        
        Application.Quit();
        print("should quit");
    }
}