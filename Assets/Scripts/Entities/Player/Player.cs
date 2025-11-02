
using UnityEngine;

public class Player : Entity, IGetCaught
{
    public Player_InputManager inputManager { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        inputManager = GetComponent<Player_InputManager>();

        idleState = new Player_IdleState(stateMachine, PlayerAnimationStrings.IDLE_ANIM, this);
        walkState = new Player_WalkState(stateMachine, PlayerAnimationStrings.WALK_ANIM, this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.InitializeState(idleState);
    }

    public void DisableInputManager()
    {
        inputManager.DisableMoveInput();
    }

    public Transform GetTransform() => transform;
}
