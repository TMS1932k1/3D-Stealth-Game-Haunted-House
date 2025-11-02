using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float speedMove;
    [SerializeField] public float speedRotation;

    public StateMachine stateMachine { get; protected set; }

    public AudioSource audioSource { get; protected set; }


    protected virtual void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.currentState.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdateState();
    }
}
