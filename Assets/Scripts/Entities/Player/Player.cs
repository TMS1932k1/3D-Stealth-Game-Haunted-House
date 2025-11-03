
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

    protected override void OnEnable()
    {
        base.OnEnable();

        GameManager.OnPauseGame += DisablePlayerInput;
        GameManager.OnResumeGame += EnablePlayerInput;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GameManager.OnPauseGame -= DisablePlayerInput;
        GameManager.OnResumeGame -= EnablePlayerInput;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.InitializeState(idleState);
    }

    public void OnActiveInput()
    {
        if (activeObserver.activeTarget == null)
        {
            Debug.Log($"[{name}]: Not have target to active");
            return;
        }

        GameManager.instance.OnPlayerActive(this, activeObserver.activeTarget);
    }

    public Transform GetTransform() => transform;

    private void EnablePlayerInput() => inputManager.EnablePlayerInput(true);

    private void DisablePlayerInput() => inputManager.EnablePlayerInput(false);
}
