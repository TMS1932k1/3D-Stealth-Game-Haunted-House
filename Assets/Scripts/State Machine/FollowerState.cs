using UnityEngine;

public class FollowerState : EntityState
{
    protected Follower follower;


    public FollowerState(StateMachine stateMachine, string nameState, Follower follower) : base(stateMachine, nameState, follower)
    {
        this.follower = follower;
    }
}
