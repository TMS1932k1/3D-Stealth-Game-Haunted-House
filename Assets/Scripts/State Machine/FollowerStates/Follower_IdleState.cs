using UnityEngine;

public class Follower_IdleState : FollowerState
{
    public Follower_IdleState(StateMachine stateMachine, string nameState, Follower follower) : base(stateMachine, nameState, follower)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (follower.followerMovement.IsMoving())
        {
            stateMachine.ChangeState(follower.walkState);
        }
    }
}
