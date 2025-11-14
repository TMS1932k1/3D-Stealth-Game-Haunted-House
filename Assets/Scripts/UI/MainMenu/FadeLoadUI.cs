using System.Collections;
using UnityEngine;

public class FadeLoadUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;

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

    public void ShowFadeLoadUI()
    {
        if (displayUICoroutine != null)
            StopCoroutine(displayUICoroutine);

        displayUICoroutine = StartCoroutine(ShowUICo());
    }

    private IEnumerator ShowUICo()
    {
        cg.blocksRaycasts = true;
        cg.interactable = true;

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            cg.alpha = timer / fadeDuration;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
