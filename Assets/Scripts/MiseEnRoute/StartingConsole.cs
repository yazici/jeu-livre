using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingConsole : MonoBehaviour
{

    public InputField inputField;
    public Text consoleText;
    public ScrollRect scrollRect;

    private bool isUserValide = false;

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
        if (!isUserValide)
        {
            if (inputField.text == "aurorechamrouge")
            {
                consoleText.text = consoleText.text + " " + inputField.text + "\nIdentifiant valide\n\nMot de passe :";
                cleanInputField();
                isUserValide = true;
            }
            else
            {
                consoleText.text = consoleText.text + " " + inputField.text + "\n\nUtilisateur non reconnu.\n\nIdentifiant : ";
                cleanInputField();
            }
        }else
        {
            if (inputField.text == "%SRghatN895")
            {
                consoleText.text = consoleText.text + "\n\nUtilisateur connecté.";
                cleanInputField();

                SceneManager.LoadScene("Mise en route");

            }
            else
            {
                consoleText.text = consoleText.text + "\n\nERREUR.\nMot de passe :";
                cleanInputField();
            }
        }
       
    }

    public void cleanInputField()
    {
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }
}
