using UnityEngine;

public class Caught_Observer : Observer<IGetCaught>
{
    private bool isCaughted;

    protected override bool ConditionToRayCast() => !isCaughted;

    protected override void OnRayCastHit(IGetCaught target)
    {
        isCaughted = true;
        GameManager.instance.OnPlayerCaughted(target);
    }
}
