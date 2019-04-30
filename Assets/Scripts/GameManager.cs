using JetBrains.Annotations;
using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager m_Instance;

    public MainSettings m_MainSettings;

    [HideInInspector] public bool m_CinematicMode;

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
        HideCursor();
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UIManager.m_Instance.m_Reticule.SetActive(false);
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.m_Instance.m_Reticule.SetActive(true);
    }

    [UsedImplicitly]
    public void Quit()
    {
        print("should quit");
        Application.Quit();
    }
}