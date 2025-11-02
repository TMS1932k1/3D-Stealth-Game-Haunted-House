using UnityEngine;

public class Follower_WalkState : FollowerState
{
    public Follower_WalkState(StateMachine stateMachine, string nameState, Follower follower) : base(stateMachine, nameState, follower)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        AudioManager.instance.PlayAudioClip(follower.audioSource, AudioClipDataNameStrings.FOOTSTEPS_AUDIO);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Cancle State
        if (!follower.followerMovement.IsMoving())
        {
            stateMachine.ChangeState(follower.idleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        AudioManager.instance.StopAudioClip(follower.audioSource);
    }
}
