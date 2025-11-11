using System.Collections;
using UnityEngine;

public enum EUiType
{
    None,
    Caught,
    Won,
    Dialogue,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private EUiType currentUI;

    [Header("Fade UI")]
    [SerializeField] private CanvasGroup exitCanvasGroup;
    [SerializeField] private CanvasGroup caughtCanvasGroup;
    [SerializeField] public float fadeDuration = 1f;
    [SerializeField] public float displayImageDuration = 1f;
    private Coroutine showEndLevelCoroutine;

    [Header("Dialogue UI")]
    [SerializeField] private CanvasGroup dialogueCanvasGroup;
    private Animator dialogueAnim;


    private void Awake()
    {
        instance = this;

        dialogueAnim = dialogueCanvasGroup.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        currentUI = EUiType.None;
    }

    public void ShowWonUI()
    {
        currentUI = EUiType.Won;
        HandleEndLevel(exitCanvasGroup, false);
    }

    public void ShowCaughtUI()
    {
        currentUI = EUiType.Caught;
        HandleEndLevel(caughtCanvasGroup, true);
    }

    public void ShowDialogueUI(DialogueSO dialogueList)
    {
        currentUI = EUiType.Dialogue;

        dialogueCanvasGroup.GetComponent<DialogueUI>().ShowDialogue(dialogueList);
        dialogueAnim.SetTrigger(DialogueAnimationStrings.SHOW_TRIGGER);
    }

    public void HideDialogueUI()
    {
        currentUI = EUiType.None;

        dialogueAnim.SetTrigger(DialogueAnimationStrings.HIDE_TRIGGER);
        GameManager.instance.OnFinishActive();
    }

    public void OnConfirmInput()
    {
        switch (currentUI)
        {
            case EUiType.Won:
                exitCanvasGroup.GetComponent<IInputAction>()?.OnConfirmInput();
                break;

            case EUiType.Caught:
                caughtCanvasGroup.GetComponent<IInputAction>()?.OnConfirmInput();
                break;

            case EUiType.Dialogue:
                dialogueCanvasGroup.GetComponent<IInputAction>()?.OnConfirmInput();
                break;

            default:
                return;
        }
    }

    public void OnBackInput()
    {
        switch (currentUI)
        {
            case EUiType.Won:
                exitCanvasGroup.GetComponent<IInputAction>()?.OnExitInput();
                break;

            case EUiType.Caught:
                caughtCanvasGroup.GetComponent<IInputAction>()?.OnExitInput();
                break;

            case EUiType.Dialogue:
                dialogueCanvasGroup.GetComponent<IInputAction>()?.OnExitInput();
                break;

            default:
                return;
        }
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

        currentUI = EUiType.None;
    }
}
