using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomBinaryText : MonoBehaviour
{
    public Text text;

    private string binaryText;
    private string[] binaryString = { "1", "0" };

    private bool isWriting = true; 

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
            if (binaryText.Length == 104)
            {
                binaryText = binaryText.Substring(26);
            }

            yield return new WaitForSeconds(.02f);


        }
    }


}
