using System.Collections;
using UnityEngine;


public class IngameUIManager : MonoBehaviour
{
    public static IngameUIManager instance;
    private EIngameUI currentUI;

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
        currentUI = EIngameUI.None;
    }

    public void ShowWonUI()
    {
        currentUI = EIngameUI.Won;
        HandleEndLevel(exitCanvasGroup, false);
    }

    public void ShowCaughtUI()
    {
        currentUI = EIngameUI.Caught;
        HandleEndLevel(caughtCanvasGroup, true);
    }

    public void ShowDialogueUI(DialogueSO dialogueList)
    {
        currentUI = EIngameUI.Dialogue;

        dialogueCanvasGroup.GetComponent<DialogueUI>().ShowDialogue(dialogueList);
        dialogueAnim.SetTrigger(DialogueAnimationStrings.SHOW_TRIGGER);
    }

    public void HideDialogueUI()
    {
        currentUI = EIngameUI.None;

        dialogueAnim.SetTrigger(DialogueAnimationStrings.HIDE_TRIGGER);
        GameManager.instance.OnFinishActive();
    }

    public void OnConfirmInput()
    {
        switch (currentUI)
        {
            case EIngameUI.Won:
                exitCanvasGroup.GetComponent<IInputAction>()?.OnConfirmInput();
                break;

            case EIngameUI.Caught:
                caughtCanvasGroup.GetComponent<IInputAction>()?.OnConfirmInput();
                break;

            case EIngameUI.Dialogue:
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
            case EIngameUI.Won:
                exitCanvasGroup.GetComponent<IInputAction>()?.OnExitInput();
                break;

            case EIngameUI.Caught:
                caughtCanvasGroup.GetComponent<IInputAction>()?.OnExitInput();
                break;

            case EIngameUI.Dialogue:
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

        currentUI = EIngameUI.None;
    }
}
