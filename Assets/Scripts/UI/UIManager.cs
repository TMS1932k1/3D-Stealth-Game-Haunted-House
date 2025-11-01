using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private CanvasGroup exitBackgroundImageCanvasGroup;
    [SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup;
    [SerializeField] public float fadeDuration = 1f;
    [SerializeField] public float displayImageDuration = 1f;

    private float timer;
    private Coroutine showEndLevelCoroutine;


    private void Awake()
    {
        instance = this;
    }

    public void ShowWonUI()
    {
        HandleEndLevel(exitBackgroundImageCanvasGroup, false);
    }

    public void ShowCaughtUI()
    {
        HandleEndLevel(caughtBackgroundImageCanvasGroup, true);
    }


    private void HandleEndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        if (showEndLevelCoroutine != null)
            StopCoroutine(showEndLevelCoroutine);

        showEndLevelCoroutine = StartCoroutine(ShowEndLevelCo(imageCanvasGroup, doRestart));
    }

    private IEnumerator ShowEndLevelCo(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        float timer = 0;
        while (timer < fadeDuration + displayImageDuration)
        {
            timer += Time.deltaTime;
            imageCanvasGroup.alpha = timer / fadeDuration;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (doRestart)
        {
            Debug.Log($"[{name}]: Restart level");
            GameManager.instance.RestartLevel();
        }
        else
        {
            Debug.Log($"[{name}]: Quit Application");
            GameManager.instance.QuitGame();
        }
    }
}
