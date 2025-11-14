using System.Collections;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    private CanvasGroup cg;
    private Coroutine displayUICoroutine;

    private LevelItemUI[] levelItemUIs;
    private int currentLevelIndex = 0;


    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        levelItemUIs = GetComponentsInChildren<LevelItemUI>();
    }

    private void Start()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;

        DisplayLevelItems();
    }

    public void OnSelectInput(Vector2 selectInput)
    {
        int calculateIndex = currentLevelIndex;

        if (selectInput.y != 0)
        {
            int direction = (int)-Mathf.Sign(selectInput.y);
            calculateIndex = (calculateIndex + direction * 3 + levelItemUIs.Length) % levelItemUIs.Length;
        }
        else if (selectInput.x != 0)
        {
            int direction = (int)Mathf.Sign(selectInput.x);
            calculateIndex = (calculateIndex + direction + levelItemUIs.Length) % levelItemUIs.Length;
        }

        if (calculateIndex <= GameManager.instance.passedLevelIndex + 1)
        {
            currentLevelIndex = calculateIndex;
            DisplayLevelItems();
        }
    }

    public void OnBackInput()
    {
        AudioManager.instance.PlayUiAudio(AudioClipDataNameStrings.SELECT_AUDIO);
        SetCurrentIndex(0);
        HandleHideUI(cg);
    }

    public void OnConfirmInput()
    {
        if (currentLevelIndex > GameManager.instance.passedLevelIndex + 1)
        {
            Debug.Log($"[{name}]: Level isn't actived");
            return;
        }

        Debug.Log($"[{name}]: Player selected {name}");
        AudioManager.instance.PlayUiAudio(AudioClipDataNameStrings.SELECT_AUDIO);

        MainMenuUIManager.instance.ShowFadeLoadUI();
        Invoke(nameof(LoadToLevel), 1f); // Should after fade duration = 0.5s
    }

    private void LoadToLevel()
    {
        GameManager.instance.LoadToLevel(levelItemUIs[currentLevelIndex].levelData);
    }

    private void DisplayLevelItems()
    {
        for (int i = 0; i < levelItemUIs.Length; i++)
        {
            if (i == currentLevelIndex && levelItemUIs[i].isSelected == false)
                levelItemUIs[i].SetSelectedBtn();

            if (i != currentLevelIndex && levelItemUIs[i].isSelected == true)
                levelItemUIs[i].SetDefaultBtn();
        }
    }

    public void SetCurrentIndex(int index)
    {
        currentLevelIndex = index;
        DisplayLevelItems();
    }

    public void ShowLevelSelectUI()
    {
        HandleShowUI(cg);
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
