using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public Camera player;

    private bool hovered = false;
    private bool moving = false;
    private bool available = true;
    private Vector3 startingPos;
    private Quaternion startingAngle;
    private Camera cam;

    //Initialisation
    private void Start()
    {
        startingPos = transform.position;
        startingAngle = transform.rotation;
        cam = player.GetComponentInChildren<Camera>();
    }

    //Hover
    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    //Fixed Update (after hover calculation)
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // click to rotate
        {
            transform.RotateAround(transform.position, cam.transform.right, 10 * Input.GetAxisRaw("Mouse Y"));
            transform.RotateAround(transform.position, cam.transform.up, -10 * Input.GetAxisRaw("Mouse X"));
        }
    }
}

