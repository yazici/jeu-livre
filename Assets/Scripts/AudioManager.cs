using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

    public void PlaySFX(string sfxName)
    {
        AkSoundEngine.PostEvent("Play_" + sfxName, gameObject);
    }

    public void StopSFX(string sfxName)
    {
        AkSoundEngine.PostEvent("Stop_" + sfxName, gameObject);
    }

    public void SetRTPC(string rtpcName, float value)
    {
        AkSoundEngine.SetRTPCValue(rtpcName, value, gameObject);
    }
}