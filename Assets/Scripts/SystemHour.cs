using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SystemHour : MonoBehaviour
{
    public Text m_Text;

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            m_Text.text = System.DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss");
            yield return new WaitForSeconds(0.2f);
        }
    }
}