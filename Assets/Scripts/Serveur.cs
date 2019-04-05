using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serveur : MonoBehaviour
{

    public Text displayText;

    private bool hovered = false;

    public static bool plugged = false;

    public static string serverPlugged = "none";

    private void OnMouseEnter()
    {
        hovered = true;
        if (!plugged)
        {
            displayText.text = "Rebrancher";
        }
        else
        {

            if (this.gameObject.name == serverPlugged)
            {

                displayText.text = "Débrancher";
            }else
            {
                displayText.text = "";
            }
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

        if (hovered && !plugged && Input.GetMouseButtonDown(0))
        {
            plugged = true;
            serverPlugged = this.gameObject.name;
            Debug.Log(this.gameObject.name);
            displayText.text = "Débrancher";

        }
        else if (hovered && plugged && this.gameObject.name == serverPlugged && Input.GetMouseButtonDown(0))
        {
            plugged = false;
            serverPlugged = "";
            displayText.text = "Rebrancher";
        }
    }
}
