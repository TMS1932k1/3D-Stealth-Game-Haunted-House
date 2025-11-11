using UnityEngine;

public class Caught_Observer : Observer<IGetCaught>
{
    [SerializeField] private Enemy enemy;
    private bool isCaughted;


    protected override bool ConditionToRayCast() => !isCaughted;

    /// <summary>
    /// - If player have (SaltJar) item => Dismiss this enemy
    /// - Else caught player
    /// </summary>
    /// <param name="target"></param>
    protected override void OnRayCastHit(IGetCaught target)
    {
        isCaughted = true;

        if (target.HaveSaltJar() && enemy != null) // Enemies are only (Ghost) and (OneEye - Keeper)
        {
            enemy.DismissBySaltJar();
            target.DismissSaltJar();
        }
        else
        {
            GameManager.instance.OnPlayerCaughted(target);
        }
    }
}
