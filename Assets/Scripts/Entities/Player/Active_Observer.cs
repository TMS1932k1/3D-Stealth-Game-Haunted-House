using UnityEngine;

public class Active_Observer : Observer<IActive>
{
    public IActive activeTarget { get; private set; }


    protected override void OnTargetExitRange(IActive target)
    {
        base.OnTargetExitRange(target);

        activeTarget?.HideTooltip();
        activeTarget = null;
    }

    protected override bool ConditionToRayCast() => activeTarget == null;

    protected override void OnRayCastHit(IActive target)
    {
        Debug.Log($"[{name}]: Hit active target");

        activeTarget = target;
        activeTarget.ShowTooltip();
    }
}
