using UnityEngine;

public class Player_WalkState : PlayerState
{
    private Player_InputManager inputManager;
    private Transform cameraDirTranf;


    public Player_WalkState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
        inputManager = player.inputManager;
        cameraDirTranf = Camera.main.transform;
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

        HandleMoveDir();
        HandleRotation();
    }

    public override void ExitState()
    {
        base.ExitState();
        AudioManager.instance.StopAudioClip(player.audioSource);
    }

    private void HandleMoveDir()
    {
        Vector3 moveDir = Vector3.zero;

        moveDir = cameraDirTranf.forward * inputManager.verticalValue;
        moveDir += cameraDirTranf.right * inputManager.horizontalValue;
        moveDir.Normalize();
        moveDir.y = 0;

        rb.linearVelocity = moveDir * player.speedMove;
    }

    private void HandleRotation()
    {
        Vector3 targerDir = Vector3.zero;

        targerDir = cameraDirTranf.forward * inputManager.verticalValue;
        targerDir += cameraDirTranf.right * inputManager.horizontalValue;
        targerDir.Normalize();
        targerDir.y = 0;

        if (targerDir != Vector3.zero)
        {
            Quaternion targerRotation = Quaternion.LookRotation(targerDir);
            Quaternion playerRotation = Quaternion.Slerp(
                player.transform.rotation,
                targerRotation,
                player.speedRotation * Time.deltaTime
            );

            player.transform.rotation = playerRotation;
        }
    }
}
