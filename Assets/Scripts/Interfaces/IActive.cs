using UnityEngine;

public interface IActive
{
    public abstract void Active(Player player, out bool successActive);
    public abstract void ShowTooltip();
    public abstract void HideTooltip();
}
