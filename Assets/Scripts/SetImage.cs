using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (ScannerTrigger.scanImage != null)
        {
            GetComponent<Image>().sprite = ScannerTrigger.scanImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
