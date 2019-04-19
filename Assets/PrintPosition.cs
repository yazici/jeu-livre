using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PrintPosition : MonoBehaviour
{
    public Text y;
    public Text x;

    private float yRotation; 
    private float xRotation;

    public GameObject player;
    private Camera cam; 

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        yRotation = Mathf.Round(player.transform.rotation.y * 180);
        xRotation = Mathf.Round(cam.transform.rotation.x * 180);

        y.text = "y = " + yRotation;
        x.text = "x = " + xRotation;
    }
}
