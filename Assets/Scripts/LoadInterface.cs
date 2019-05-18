using System.Collections;
using UnityEngine;

public class LoadInterface : MonoBehaviour
{
    public Canvas m_RobotUI;
    public CanvasGroup m_RobotUICanvasGroup;

    private GameObject m_LoadingBar;
    private Animation m_LoadingBarAnim;

    private GameObject m_ObjectInterface;

    private CanvasGroup m_CanvasGroup;

    [SerializeField] private float m_FadeTime = 1f;

    private bool m_IsVisibleInterface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !m_IsVisibleInterface)
        {
            StartCoroutine(OpenInterface("ScannerUI", true));
        }

        if (Input.GetKeyDown(KeyCode.Escape) && m_IsVisibleInterface)
        {
            StartCoroutine(CloseInterface());
        }
    }

    public IEnumerator OpenInterface(string objectToLoad, bool playerCanMove = false)
    {
        if (m_IsVisibleInterface)
        {
            yield return StartCoroutine(CloseInterface());
        }
        
        if (!playerCanMove)
        {
            GameManager.ShowCursor();
        }

        StartCoroutine(PlayLoadingAnimation());
        StartCoroutine(AlphaFadeOut(m_RobotUICanvasGroup, 0.1f, m_FadeTime));

        yield return WaitForAnimation();

        Destroy(m_LoadingBar);

        yield return StartCoroutine(InstantiateGameObject(objectToLoad, gameObject));
        yield return StartCoroutine(AlphaFadeIn(m_CanvasGroup, 1f, m_FadeTime));

        m_IsVisibleInterface = true;
    }

    public IEnumerator CloseInterface()
    {
        m_IsVisibleInterface = false;
        GameManager.HideCursor();
        yield return StartCoroutine(AlphaFadeOut(m_CanvasGroup, 0f, m_FadeTime));
        StartCoroutine(AlphaFadeIn(m_RobotUICanvasGroup, 0.5f, m_FadeTime));
    }

    IEnumerator PlayLoadingAnimation()
    {
        Instantiate(Resources.Load("LoadingBar"));
        m_LoadingBar = GameObject.Find("LoadingBar(Clone)");
        m_LoadingBar.transform.SetParent(m_RobotUI.transform);
        m_LoadingBarAnim = m_LoadingBar.GetComponentInChildren<Animation>();
        m_LoadingBarAnim.Play();

        yield return null;
    }


    IEnumerator WaitForAnimation()
    {
        do
        {
            yield return null;
        } while (m_LoadingBarAnim.isPlaying);
    }

    IEnumerator InstantiateGameObject(string objectToLoad, GameObject gameObjectToLoad)
    {
        Instantiate(Resources.Load(objectToLoad));
        gameObjectToLoad = GameObject.Find(objectToLoad + "(Clone)");
        gameObjectToLoad.transform.SetParent(m_RobotUI.transform);
        m_CanvasGroup = gameObjectToLoad.GetComponentInChildren<CanvasGroup>();

        m_ObjectInterface = gameObjectToLoad;

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

            if (canvasGroupToFade != m_RobotUICanvasGroup)
            {
                if (canvasGroupToFade.alpha <= 0)
                {
                    Destroy(m_ObjectInterface);
                    yield break;
                }
            }

            yield return null;
        }
    }
}