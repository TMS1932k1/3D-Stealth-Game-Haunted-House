using TMPro;
using UnityEngine;


public enum EPickUpType
{
    None,
    SaltJar,
}

public class PickUp : MonoBehaviour, IObserver, IActive
{
    [SerializeField] private string idTarget;

    [SerializeField] private DialogueSO dialogue;
    [SerializeField] protected EPickUpType pickUpType;

    private TextMeshProUGUI tooltip;
    protected bool canActive = true;


    private void OnEnable()
    {
        GameManager.OnPauseGame += OnPauseGame;
        GameManager.OnResumeGame += OnResumeGame;
    }

    private void OnDisable()
    {
        GameManager.OnPauseGame -= OnPauseGame;
        GameManager.OnResumeGame -= OnResumeGame;
    }

    private void Awake()
    {
        tooltip = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        tooltip.gameObject.SetActive(false);
    }

    public virtual void Active(Player player, out bool successActive)
    {
        successActive = true;

        if (!canActive || player.playerHold == null)
        {
            Debug.Log($"[{name}]: Now player can't pick up {name}!");
            successActive = false;
            return;
        }

        if (player.playerHold.holdingType != EPickUpType.None)
        {
            Debug.Log($"[{name}]: Now player is holding item!");

            // Code Dialogue show can't hold pick up item
            player.playerHold.HoldingPickUp();
        }
        else
        {
            Debug.Log($"[{name}]: Player picked up {name}");

            // Hide object and handle player hold
            gameObject.SetActive(false);
            player.playerHold.HoldPickUp(pickUpType, dialogue);
        }

    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    public void ShowTooltip()
    {
        tooltip.gameObject.SetActive(true);
    }

    public Transform GetTransform() => transform;

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
