using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour, IInputAction
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    private DialogueSO currentDialogueData;
    private bool canInputAction;


    public void ShowDialogue(DialogueSO dialogueData)
    {
        if (dialogueData == null)
        {
            Debug.LogWarning($"[{name}]: Dialogue data is null!");
            OnExitInput();
            return;
        }

        currentDialogueData = dialogueData;
        dialogueText.text = $"{dialogueData.nameSpeaker}\n{dialogueData.dialogue}";
    }

    public void OnConfirmInput()
    {
        Debug.Log("Confirm");

        if (!canInputAction)
            return;

        if (currentDialogueData.nextDialogue != null)
        {
            ShowDialogue(currentDialogueData.nextDialogue);
            PlayDialogueAudioClip();
        }
        else
            OnExitInput();
    }

    public void OnExitInput()
    {
        if (!canInputAction)
            return;

        IngameUIManager.instance.HideDialogueUI();

        currentDialogueData = null;
        canInputAction = false;
        Debug.Log("Exit");
    }

    public void PlayDialogueAudioClip()
    {
        if (currentDialogueData == null || currentDialogueData.nameAudioClipData == null)
        {
            Debug.LogWarning($"[{name}]: Dialogue audio clip data is null!");
            return;
        }

        AudioManager.instance.PlayUiAudio(currentDialogueData.nameAudioClipData);
    }

    public void CallTriggerFinishAnimation()
    {
        canInputAction = true;
        PlayDialogueAudioClip();
    }
}
