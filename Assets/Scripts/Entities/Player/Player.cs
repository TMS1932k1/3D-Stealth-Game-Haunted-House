
using UnityEngine;

public class Player : Entity, IObserver, IGetCaught
{
    public Player_InputManager inputManager { get; private set; }

    private Active_Observer activeObserver;
    public Player_Hold playerHold { get; private set; }
    private Rigidbody rb;
    private Transform cameraDirTranf;

    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    public Player_HoldIdleState holdIdleState { get; private set; }
    public Player_HoldWalkState holdWalkState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        inputManager = GetComponent<Player_InputManager>();
        activeObserver = GetComponentInChildren<Active_Observer>();
        playerHold = GetComponent<Player_Hold>();
        rb = GetComponent<Rigidbody>();

        cameraDirTranf = Camera.main.transform;

        idleState = new Player_IdleState(stateMachine, PlayerAnimationStrings.IDLE_ANIM, this);
        walkState = new Player_WalkState(stateMachine, PlayerAnimationStrings.WALK_ANIM, this);
        holdIdleState = new Player_HoldIdleState(stateMachine, PlayerAnimationStrings.HOLD_IDLE_ANIM, this);
        holdWalkState = new Player_HoldWalkState(stateMachine, PlayerAnimationStrings.HOLD_WALK_ANIM, this);
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

    public void HandleMoveDir(bool isHold)
    {
        Vector3 moveDir = Vector3.zero;

        moveDir = cameraDirTranf.forward * inputManager.verticalValue;
        moveDir += cameraDirTranf.right * inputManager.horizontalValue;
        moveDir.Normalize();
        moveDir.y = 0;

        if (!isHold)
            rb.linearVelocity = moveDir * speedMove;
        else
            rb.linearVelocity = moveDir * speedMove * holdMutiplier;
    }

    public void HandleRotation()
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
                transform.rotation,
                targerRotation,
                speedRotation * Time.deltaTime
            );

            transform.rotation = playerRotation;
        }
    }

    public Transform GetTransform() => transform;

    public bool HaveSaltJar() => playerHold.holdingType == EPickUpType.SaltJar;

    public void DismissSaltJar() => playerHold.DismissPickUp();

    private void EnablePlayerInput() => inputManager.EnablePlayerInput(true);

    private void DisablePlayerInput() => inputManager.EnablePlayerInput(false);
}
