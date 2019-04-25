using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protocole : MonoBehaviour
{

    public Text uitext;
    public GameObject console;

    private bool hovered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Hover
    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        uitext.text = "";
        hovered = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hovered)
        {
            uitext.text = "Se connecter au terminal";
        }

        if (hovered && Input.GetMouseButtonDown(0))
        {
            console.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }

    }
}
