using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logiciel : MonoBehaviour
{

    public Button button;

    public Dropdown dropdown1, dropdown2, dropdown3;

    // Start is called before the first frame update
    void Start()
    {

        button.onClick.AddListener(Validate);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
        
    }

    private void Validate()
    {
        if(dropdown1.value == 3 && dropdown2.value == 1 && dropdown3.value == 4)
        {
            Application.Quit();
        }
    }
}
