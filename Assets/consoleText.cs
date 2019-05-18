using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class consoleText : MonoBehaviour
{

    public TextMeshProUGUI textMesh;
    public Text text; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = text.text;
    }
}
