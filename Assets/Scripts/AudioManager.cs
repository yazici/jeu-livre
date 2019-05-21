using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private Camera m_Camera;
    
    // Singleton
    public static AudioManager m_Instance;

    // Singleton initialization
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        m_Camera = Camera.main;
    }

    public void PlaySFX(string sfxName, GameObject gameObj = null)
    {
        AkSoundEngine.PostEvent("Play_" + sfxName, gameObj == null ? m_Camera.gameObject : gameObj);
    }

    public void StopSFX(string sfxName, GameObject gameObj = null)
    {
        AkSoundEngine.PostEvent("Stop_" + sfxName, gameObj == null ? m_Camera.gameObject : gameObj);
    }

    public void SetRTPC(string rtpcName, float value)
    {
        AkSoundEngine.SetRTPCValue(rtpcName, value, m_Camera.gameObject);
    }
}