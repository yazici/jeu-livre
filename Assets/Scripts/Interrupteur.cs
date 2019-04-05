using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interrupteur : MonoBehaviour
{

    public Text displayText;

    public Material skyDay;
    public Material skyNight;
    public Light sun;

    private bool hovered = false;

    public bool isDay = false;
    

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
            if (isDay)
            {
                RenderSettings.skybox = skyNight;
                isDay = false;
                sun.color = new Color(0.31f, 0.31f, 0.31f);
                //RenderSettings.ambientLight = Color.gray;
            }
            else
            {
                RenderSettings.skybox = skyDay;
                isDay = true;
                sun.color = new Color(5f, 5f, 5f);
                //RenderSettings.ambientLight = Color.white;
            }

        }
    }
}
