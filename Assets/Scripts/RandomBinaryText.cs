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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RandomString()
    {
        while(isWriting)
        {
            binaryText += binaryString[Random.Range(0, binaryString.Length)];
            text.text = binaryText;
            //float waitTime = Random.Range(2f, 10f);
            if (binaryText.Length == maxChar)
            {
                binaryText = binaryText.Substring(newCharNumber);
            }

            yield return new WaitForSeconds(.02f);


        }
    }


}
