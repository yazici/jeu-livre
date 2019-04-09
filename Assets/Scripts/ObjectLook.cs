using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectLook : MonoBehaviour
{

    public Text uitext;
    public string objectName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Hover
    private void OnMouseEnter()
    {
        uitext.text = objectName;
    }

    private void OnMouseExit()
    {
        uitext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
