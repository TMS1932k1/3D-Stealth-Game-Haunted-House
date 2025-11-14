using System.Collections;
using UnityEngine;

public class FadeEnterUI : MonoBehaviour
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
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;

        HideFadeEnterUI();
    }

    public void HideFadeEnterUI()
    {
        if (displayUICoroutine != null)
            StopCoroutine(displayUICoroutine);

        displayUICoroutine = StartCoroutine(HideUICo());
    }

    private IEnumerator HideUICo()
    {
        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            cg.alpha = timer / fadeDuration;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        cg.blocksRaycasts = false;
        cg.interactable = false;
    }
}
