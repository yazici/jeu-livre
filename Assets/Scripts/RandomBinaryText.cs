using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RandomBinaryText : MonoBehaviour
{
    public TextMeshProUGUI text;

    private string binaryText;
    private string[] binaryString = { "1", "0" };

    private bool isWriting = true;

    public int maxChar;
    public int newCharNumber;

    private void Start()
    {
        StartCoroutine(RandomString());
    }

    IEnumerator RandomString()
    {
        while (isWriting)
        {
            binaryText += binaryString[Random.Range(0, binaryString.Length)];
            text.text = binaryText;
            if (binaryText.Length == maxChar)
            {
                binaryText = binaryText.Substring(newCharNumber);
            }

            yield return new WaitForSeconds(.02f);
        }
    }
}