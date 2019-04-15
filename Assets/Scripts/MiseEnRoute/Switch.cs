using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{

    public Text displayText;
    private bool hovered = false;

    public static bool switched = false;
    

    private void OnMouseEnter()
    {
        hovered = true;
        if (true)
        {
            displayText.text = "Appuyer sur l'interrupteur";
        }

    }

    private void OnMouseExit()
    {
        hovered = false;
        displayText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (hovered && Serveur.serverPlugged.Equals("Server2"))
            {
                switched = true;
                Debug.Log("switched");
            }

        }
    }
}
