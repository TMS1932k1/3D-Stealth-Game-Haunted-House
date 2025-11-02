using TMPro;
using UnityEngine;

public class Npc : Entity, IObserver, IActive
{
    [Header("NPC")]
    [SerializeField] protected TextMeshProUGUI tooltip;

    public virtual void Active(Player player)
    {
        Debug.Log($"[{name}]: Npc active");
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
}
