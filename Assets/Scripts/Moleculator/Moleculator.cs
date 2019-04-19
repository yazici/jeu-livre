using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Moleculator : MonoBehaviour
{

    public RectTransform dropZone0;
    public RectTransform dropZone1;
    public RectTransform dropZone2;

    public GameObject[] goodItems;

    public Text consoleText;
    public Button validateButton;

    private int score = 0;
    private static GameObject[] droppedItems;


    // Start is called before the first frame update
    void Start()
    {
        droppedItems = new GameObject[3];
        validateButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void doDrag(GameObject gameObject)
    {
        gameObject.transform.position = Input.mousePosition;
        print("on drag sur : " + gameObject);

    }

    public void doEndDrag(GameObject gameObject)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone0, Input.mousePosition))
        {
            if (droppedItems[0] != null)
            {
                droppedItems[0].transform.localPosition = Vector3.zero;
            }
            droppedItems[0] = gameObject;
            gameObject.transform.position = dropZone0.position;

        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(dropZone1, Input.mousePosition))
        {
            if (droppedItems[1] != null)
            {
                droppedItems[1].transform.localPosition = Vector3.zero;
            }
            gameObject.transform.position = dropZone1.position;
            droppedItems[1] = gameObject;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(dropZone2, Input.mousePosition))
        {
            if (droppedItems[2] != null)
            {
                droppedItems[2].transform.localPosition = Vector3.zero;
            }
            gameObject.transform.position = dropZone2.position;
            droppedItems[2] = gameObject;
        }
        else
        {
            gameObject.transform.localPosition = Vector3.zero;
        }

        if(droppedItems[0] != null && droppedItems[1] != null && droppedItems[2] != null)
        {
            validateButton.interactable = true;
        }
        else
        {
            validateButton.interactable = false;
        }

    }
   

    public void cancelButton()
    {
       
        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone0, Input.mousePosition))
        {
           if(droppedItems[0] != null)
            {
                droppedItems[0].transform.localPosition = Vector3.zero;
                droppedItems[0] = null;
            }

        }
        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone1, Input.mousePosition))
        {
            if (droppedItems[1] != null)
            {
                droppedItems[1].transform.localPosition = Vector3.zero;
                droppedItems[1] = null;
            }

        }
        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone2, Input.mousePosition))
        {
            if (droppedItems[2] != null)
            {
                droppedItems[2].transform.localPosition = Vector3.zero;
                droppedItems[2] = null;
            }

        }

        validateButton.interactable = false;
    }

    public void doReset()
    {
        if (droppedItems[0] != null)
        {
            droppedItems[0].transform.localPosition = Vector3.zero;
            droppedItems[0] = null;
        }
        if (droppedItems[1] != null)
        {
            droppedItems[1].transform.localPosition = Vector3.zero;
            droppedItems[1] = null;
        }
        if (droppedItems[2] != null)
        {
            droppedItems[2].transform.localPosition = Vector3.zero;
            droppedItems[2] = null;
        }

        validateButton.interactable = false;

    }

    public void Validate()
    {

        score = 0;

        for(int i=0; i<3; i++)
        {
            for (int j=0; j<3; j++) {

                if (droppedItems[i] == goodItems[j])
                {
                    score++;
                }
            }
        }

        switch (score)
        {
            case 0:
                consoleText.text += "\n\nAnalyse de la solution...\n\nRésultat : solution valide à 0%";
                break;
            case 1:
                consoleText.text += "\n\nAnalyse de la solution...\n\nRésultat : solution valide à 33%";
                break;
            case 2:
                consoleText.text += "\n\nAnalyse de la solution...\n\nRésultat : solution valide à 66%";
                break;
            case 3:
                consoleText.text += "\n\nAnalyse de la solution...\n\nRésultat : solution valide à 100%";
                break;
            default:
                consoleText.text += "\n\nAnalyse de la solution...\n\nERREUR - Veuillez recommencer la procédure";
                break;
        }

    }

    public void setFocus(Transform window)
    {
        window.SetSiblingIndex(1);
    }
}
