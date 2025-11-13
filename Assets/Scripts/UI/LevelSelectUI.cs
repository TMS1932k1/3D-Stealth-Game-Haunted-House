using System.Collections;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    private CanvasGroup cg;
    private Coroutine displayUICoroutine;


    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public void ShowLevelSelectUI()
    {
        HandleShowUI(cg);
    }

    public void OnBackInput()
    {
        AudioManager.instance.PlayUiAudio(AudioClipDataNameStrings.SELECT_AUDIO);
        HandleHideUI(cg);
    }

    private void HandleShowUI(CanvasGroup canvasGroup)
    {
        cg.blocksRaycasts = true;
        cg.interactable = true;

        if (displayUICoroutine != null)
            StopCoroutine(displayUICoroutine);

        displayUICoroutine = StartCoroutine(ShowUICo(canvasGroup));
    }

    private IEnumerator ShowUICo(CanvasGroup canvasGroup)
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = timer / fadeDuration;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void HandleHideUI(CanvasGroup canvasGroup)
    {
        cg.blocksRaycasts = false;
        cg.interactable = false;

        if (displayUICoroutine != null)
            StopCoroutine(displayUICoroutine);

        displayUICoroutine = StartCoroutine(HideUICo(canvasGroup));
    }

    private IEnumerator HideUICo(CanvasGroup canvasGroup)
    {

        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            canvasGroup.alpha = timer / fadeDuration;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
