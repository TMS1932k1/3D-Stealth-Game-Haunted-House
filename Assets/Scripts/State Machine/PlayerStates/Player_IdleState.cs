using UnityEngine;

public class Player_IdleState : PlayerState
{
    public Player_IdleState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        rb.linearVelocity = Vector3.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (player.inputManager.IsWalking())
        {
            stateMachine.ChangeState(player.walkState);
        }
    }
}
