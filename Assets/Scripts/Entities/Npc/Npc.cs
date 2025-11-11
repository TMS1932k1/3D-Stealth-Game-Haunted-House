using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : Entity, IObserver, IActive
{
    [Header("NPC")]
    [SerializeField] private bool isFirstActive = true;
    [Space]
    [SerializeField] private DialogueSO firstDialogue;
    [SerializeField] private List<DialogueSO> dialogueList;

    private TextMeshProUGUI tooltip;
    protected bool canActive = true;


    protected override void Awake()
    {
        base.Awake();
        tooltip = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameManager.OnPauseGame += OnPauseGame;
        GameManager.OnResumeGame += OnResumeGame;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GameManager.OnPauseGame -= OnPauseGame;
        GameManager.OnResumeGame -= OnResumeGame;
    }

    protected override void Start()
    {
        base.Start();
        tooltip.gameObject.SetActive(false);
    }

    /// <summary>
    ///  - Set dialogue to show: 
    ///     + if this is first talk with npc, get (firstDialogue)
    ///     + else get random from (dialogueList)
    ///  - Handle logic of npc in children class
    /// </summary>
    /// <param name="player"></param>
    public virtual void Active(Player player, out bool successActive)
    {
        successActive = true;

        if (!canActive)
        {
            Debug.Log($"[{name}]: Now player can't active NPC {name}!");
            successActive = false;
            return;
        }

        Debug.Log($"[{name}]: Player active NPC {name}");

        // Handle get dialogueData
        DialogueSO dialogueShow = isFirstActive ? firstDialogue : GetRandomDiaLogue();
        if (dialogueShow == null)
        {
            Debug.LogWarning($"[{name}]: Dialogue list is null or empty");
            successActive = false;
            return;
        }

        // Code UI to show dialogue
        UIManager.instance.ShowDialogueUI(dialogueShow);
        isFirstActive = false;
    }

    public Transform GetTransform() => transform;

    public void ShowTooltip()
    {
        tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    private DialogueSO GetRandomDiaLogue()
    {
        if (dialogueList == null || dialogueList.Count <= 0)
            return null;

        int random = Random.Range(0, dialogueList.Count);
        return dialogueList[random];
    }

    protected virtual void OnPauseGame()
    {
        tooltip.gameObject.SetActive(false);
        canActive = false; // Avoid active in Dialogue is processing
    }

    protected virtual void OnResumeGame()
    {
        canActive = true;
    }
}
