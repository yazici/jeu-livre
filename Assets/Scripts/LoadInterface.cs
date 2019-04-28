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

    [SerializeField] private float fadeTime = 1.5f;

    private bool visibleInterface;
    private bool activeInterface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           StartCoroutine(OpenInterface("ScannerUI",false));
        }
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeInterface)
                StartCoroutine(CloseInterface());
        }
    }

    public IEnumerator OpenInterface(string objectToLoad)
    {
        GameManager.ShowCursor();

        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(AlphaFadeOut(robotUICanvasGroup, 0.2f, fadeTime));

        yield return WaitForAnimation();

        Destroy(loadingBar);

        yield return StartCoroutine(InstantiateGameObject(objectToLoad, gameObject));
        yield return StartCoroutine(AlphaFadeIn(canvasGroup, 1f, fadeTime));

        visibleInterface = true;
        activeInterface = true;
    }

    public IEnumerator OpenInterface(string objectToLoad, bool isFixed)
    {
        if (!isFixed)
        {
        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(AlphaFadeOut(robotUICanvasGroup, 0.2f, fadeTime));

        yield return WaitForAnimation();

        Destroy(loadingBar);

        yield return StartCoroutine(InstantiateGameObject(objectToLoad, gameObject));
        yield return StartCoroutine(AlphaFadeIn(canvasGroup, 1f, fadeTime));

        visibleInterface = false;
        activeInterface = true;
        }else
        {
            OpenInterface(objectToLoad);
        }
    }

    public IEnumerator CloseInterface()
    {
        visibleInterface = false;
        activeInterface = false;
        GameManager.HideCursor();
        yield return StartCoroutine(AlphaFadeOut(canvasGroup, 0f, fadeTime));
        StartCoroutine(AlphaFadeIn(robotUICanvasGroup, 1f, fadeTime));
    }

    IEnumerator PlayLoadingAnimation()
    {
        Instantiate(Resources.Load("LoadingBar"));
        loadingBar = GameObject.Find("LoadingBar(Clone)");
        loadingBar.transform.SetParent(robotUI.transform);
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
