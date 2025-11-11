using UnityEngine;

public class Player_HoldIdleState : Player_HoldState
{
    public Player_HoldIdleState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Cancle State
        if (inputManager.IsWalking())
        {
            stateMachine.ChangeState(player.holdWalkState);
        }
    }
}
