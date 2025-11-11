using UnityEngine;

public class Player_WalkState : PlayerState
{
    public Player_WalkState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        AudioManager.instance.PlayAudioClip(player.audioSource, AudioClipDataNameStrings.FOOTSTEPS_AUDIO);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Cancle State
        if (!inputManager.IsWalking())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        player.HandleMoveDir(false);
        player.HandleRotation();
    }

    public override void ExitState()
    {
        base.ExitState();
        AudioManager.instance.StopAudioClip(player.audioSource);
    }
}
