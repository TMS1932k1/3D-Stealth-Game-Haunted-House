using UnityEngine;

public abstract class Player_NormalState : PlayerState
{
    private Player_Hold playerHold;


    public Player_NormalState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
        playerHold = player.GetComponent<Player_Hold>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (playerHold.holdingType != EPickUpType.None)
        {
            stateMachine.ChangeState(player.holdIdleState);
        }
    }
}
