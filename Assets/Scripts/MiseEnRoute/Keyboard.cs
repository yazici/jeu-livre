using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{

    public Text displayText;

    public Light lighteuh;

    private bool hovered = false;

    private void OnMouseEnter()
    {
        hovered = true;

        displayText.text = "Réactiver le système";
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

        if (hovered && Switch.switched && Input.GetMouseButtonDown(0))
        {
            lighteuh.intensity = 1.5f;
            Debug.Log("victoire");
        }

    }
}
