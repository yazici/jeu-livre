using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class canvasAlpha : MonoBehaviour
{

    public Canvas moléculatorCanvas;
    private CanvasGroup canvasGroup;
    public int fadeTime; 
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = moléculatorCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; 
        moléculatorCanvas.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ActivateMoleculator();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            DeactivateMoleculator();
        }


    }

    IEnumerator AlphaFadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 1 * Time.deltaTime / fadeTime;
            yield return null; 
        }
    }

    IEnumerator AlphaFadeOut()
    {
        while (canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= 1 * Time.deltaTime / fadeTime;
            yield return null;
        }

        moléculatorCanvas.enabled = false; 
    }

    public void ActivateMoleculator()
    {
        moléculatorCanvas.enabled = true;
        StartCoroutine(AlphaFadeIn());
        if(canvasGroup.alpha >= 1)
        {
            StopCoroutine(AlphaFadeIn());
        }
    }

    public void DeactivateMoleculator()
    {
        StartCoroutine(AlphaFadeOut());
    }
}
