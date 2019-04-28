using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemHour : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator UpdateTime()
    {
        while (true)
        {

            text.text = System.DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss");
            yield return new WaitForSeconds(0.2f);
            

        }
    }


}
