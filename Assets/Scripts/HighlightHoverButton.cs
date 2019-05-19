using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color m_HighlightColor;
    
    private TMP_Text m_Text;
    private Color m_OriginalColor;

    private void Awake()
    {
        m_Text = GetComponentInChildren<TMP_Text>();
        m_OriginalColor = m_Text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Text.color = m_HighlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Text.color = m_OriginalColor;
    }
}