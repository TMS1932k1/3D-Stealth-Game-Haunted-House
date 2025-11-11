using UnityEngine;

public class Player_HoldWalkState : Player_HoldState
{
    public Player_HoldWalkState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        AudioManager.instance.PlayAudioClip(player.audioSource, AudioClipDataNameStrings.FOOTSTEPS_AUDIO, player.holdMutiplier - 0.1f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Cancle State
        if (!inputManager.IsWalking())
        {
            stateMachine.ChangeState(player.holdIdleState);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        player.HandleMoveDir(true);
        player.HandleRotation();
    }

    public override void ExitState()
    {
        base.ExitState();
        AudioManager.instance.StopAudioClip(player.audioSource);
    }
}
