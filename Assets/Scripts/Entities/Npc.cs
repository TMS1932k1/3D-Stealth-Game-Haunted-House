using TMPro;
using UnityEngine;

public class Npc : Entity
{
    [Header("NPC")]
    [SerializeField] protected TextMeshProUGUI tooltip;


    public virtual void ActiveNpc()
    {
        Debug.Log($"[{name}]: Active Npc");
    }
}
