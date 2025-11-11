using UnityEngine;

public class Player_HoldState : PlayerState
{
    private Player_Hold playerHold;


    public Player_HoldState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
        playerHold = player.GetComponent<Player_Hold>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (playerHold.holdingType == EPickUpType.None)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
