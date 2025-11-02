
using UnityEngine;

public class Player : Entity, IObserver, IGetCaught
{
    public Player_InputManager inputManager { get; private set; }

    private Active_Observer activeObserver;

    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        inputManager = GetComponent<Player_InputManager>();
        activeObserver = GetComponentInChildren<Active_Observer>();

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

    public void Active()
    {
        if (activeObserver.activeTarget == null)
        {
            Debug.Log($"[{name}]: Not have target to active");
            return;
        }

        activeObserver.activeTarget.Active(this);
    }

    public Transform GetTransform() => transform;
}
