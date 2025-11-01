using UnityEngine;

public class Player : MonoBehaviour
{
    public Player_InputManager inputManager { get; private set; }

    [Header("Movement")]
    [SerializeField] public float speedMove;
    [SerializeField] public float speedRotation;

    #region StateMachine
    public StateMachine stateMachine { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    #endregion

    public AudioSource audioSource { get; private set; }


    private void Awake()
    {
        inputManager = GetComponent<Player_InputManager>();
        audioSource = GetComponentInChildren<AudioSource>();

        stateMachine = new StateMachine();
        idleState = new Player_IdleState(stateMachine, PlayerAnimationStrings.IDLE_ANIM, this);
        walkState = new Player_WalkState(stateMachine, PlayerAnimationStrings.WALK_ANIM, this);
    }

    private void Start()
    {
        stateMachine.InitializeState(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdateState();
    }
}
