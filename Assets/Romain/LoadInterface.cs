using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LoadInterface : MonoBehaviour
{
    public Canvas robotUI;
    public CanvasGroup robotUICanvasGroup;

    private GameObject loadingBar;
    private Animation loadingBarAnim;

    private GameObject objectInterface;

    private CanvasGroup canvasGroup;

    public IEnumerator OpenInterface(string objectToLoad)
    {
        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(AlphaFadeOut(robotUICanvasGroup, 0.2f, 3f));

        yield return WaitForAnimation();

        Destroy(loadingBar);

        yield return StartCoroutine(InstantiateGameObject(objectToLoad, gameObject));
        StartCoroutine(AlphaFadeIn(canvasGroup, 1f, 3f));
 

        yield return null; 
    }

    public IEnumerator CloseInterface()
    {
        yield return StartCoroutine(AlphaFadeOut(canvasGroup, 0f, 3f));
        StartCoroutine(AlphaFadeIn(robotUICanvasGroup, 1f, 3f));
    }

    IEnumerator PlayLoadingAnimation()
    {
        Instantiate(Resources.Load("LoadingBar"));
        loadingBar = GameObject.Find("LoadingBar(Clone)");
        loadingBarAnim = loadingBar.GetComponentInChildren<Animation>();
        loadingBarAnim.Play();

        yield return null;
    }


    IEnumerator WaitForAnimation()
    {
        do
        {
            yield return null;
        } while (loadingBarAnim.isPlaying);
    }

    IEnumerator InstantiateGameObject(string objectToLoad, GameObject gameObjectToLoad)
    {
        Instantiate(Resources.Load(objectToLoad));
        gameObjectToLoad = GameObject.Find(objectToLoad + "(Clone)");
        gameObjectToLoad.transform.SetParent(robotUI.transform);
        canvasGroup = gameObjectToLoad.GetComponentInChildren<CanvasGroup>();

        objectInterface = gameObjectToLoad;

        yield return null;
    }

    IEnumerator AlphaFadeIn(CanvasGroup canvasGroupToFade, float alphaValue, float fadeTime)
    {
        while (canvasGroupToFade.alpha < alphaValue)
        {
            canvasGroupToFade.alpha += 1 * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator AlphaFadeOut(CanvasGroup canvasGroupToFade, float alphaValue, float fadeTime)
    {
        while (canvasGroupToFade.alpha >= alphaValue)
        {
            canvasGroupToFade.alpha -= 1 * Time.deltaTime / fadeTime;

            if (canvasGroupToFade != robotUICanvasGroup)
            {
                if (canvasGroupToFade.alpha <= 0)
                {
                    Destroy(objectInterface);
                    yield break;
                }
            }

            yield return null;
        }
    }




}
