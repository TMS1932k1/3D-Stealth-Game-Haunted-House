using UnityEngine;

public interface IActive
{
    public abstract void Active(Player player);
    public abstract void ShowTooltip();
    public abstract void HideTooltip();
}
