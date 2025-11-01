using UnityEngine;

public abstract class PlayerState
{
    private string nameState;
    protected StateMachine stateMachine;
    protected Player player;

    protected Rigidbody rb;
    private Animator anim;


    public PlayerState(StateMachine stateMachine, string nameState, Player player)
    {
        this.stateMachine = stateMachine;
        this.nameState = nameState;
        this.player = player;

        rb = player.GetComponent<Rigidbody>();
        anim = player.GetComponentInChildren<Animator>();
    }

    public virtual void EnterState()
    {
        anim.SetBool(nameState, true);
    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }

    public virtual void ExitState()
    {
        anim.SetBool(nameState, false);
    }
}
