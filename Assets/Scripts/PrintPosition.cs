using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PrintPosition : MonoBehaviour
{
    public Text position; 

    private float yRotation; 
    private float xRotation;

    private GameObject player;
    private Camera cam; 

    void Start()
    {
        cam = Camera.main; 
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        yRotation = Mathf.Round(player.transform.rotation.y * 180);
        xRotation = Mathf.Round(cam.transform.rotation.x * 180);

        position.text = "y = " + yRotation + "\nx = " + xRotation; 
    }
}
