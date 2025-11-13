using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string idTarget;

    [Header("Movement")]
    [SerializeField] public float speedMove;
    [SerializeField] public float speedRotation;
    [Range(0, 1)]
    [SerializeField] public float holdMutiplier = 0.7f;

    public StateMachine stateMachine { get; protected set; }

    public AudioSource audioSource { get; protected set; }


    protected virtual void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();

        stateMachine = new StateMachine();
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

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
