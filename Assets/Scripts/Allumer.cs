using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allumer : MonoBehaviour
{

    public VLight vlight;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
        if (vlight.lightMultiplier < 0.4)
        {


            vlight.lightMultiplier += 0.001f;
        }


    }
}
