using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager m_Instance;

    // UI elements
    [SerializeField] private Image m_ReticuleOn;
    [SerializeField] private Image m_ReticuleOff;

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
    }

    // Set the reticule black or white
    public void SetReticule(bool active = true)
    {
        m_ReticuleOn.enabled = active;
        m_ReticuleOff.enabled = !active;
    }

    // // Hide the reticule when holding an object
    // public void HideReticule()
    // {
    //     m_ReticuleOn.enabled = false;
    //     m_ReticuleOff.enabled = false;
    // }
}