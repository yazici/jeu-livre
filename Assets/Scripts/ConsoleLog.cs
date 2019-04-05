using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConsoleLog : MonoBehaviour
{

    public InputField inputField;

    public Text consoleText;

    public ScrollRect scrollRect;

    private bool isuserok = false;

    // Start is called before the first frame update
    void Start()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayConsoleText()
    {
        if (!isuserok)
        {
            if (inputField.text == "aurorechamrouge")
            {
                consoleText.text = consoleText.text + " " + inputField.text + "\nIdentifiant valide\n\nMot de passe :";
                //scrollRect.normalizedPosition = new Vector2(0, 0);
                inputField.text = "";
                inputField.Select();
                inputField.ActivateInputField();
                isuserok = true;
            }
            else
            {
                consoleText.text = consoleText.text + " " + inputField.text + "\n\nUtilisateur non reconnu.\n\nIdentifiant : ";
                //scrollRect.normalizedPosition = new Vector2(0, 0);
                inputField.text = "";
                inputField.Select();
                inputField.ActivateInputField();
            }
        }else
        {
            if (inputField.text == "%SRghatN895")
            {
                consoleText.text = consoleText.text + "\n\nUtilisateur connecté.";
                //scrollRect.normalizedPosition = new Vector2(0, 0);
                inputField.text = "";
                inputField.Select();
                inputField.ActivateInputField();

                SceneManager.LoadScene("pouet");

            }
            else
            {
                consoleText.text = consoleText.text + "\n\nERREUR.\nMot de passe :";
                //scrollRect.normalizedPosition = new Vector2(0, 0);
                inputField.text = "";
                inputField.Select();
                inputField.ActivateInputField();
            }
        }
       
    }
}
