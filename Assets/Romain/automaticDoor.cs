using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automaticDoor : MonoBehaviour
{

    public Animator doorAnim; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("isOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("isClosed");
        }
    }
}
