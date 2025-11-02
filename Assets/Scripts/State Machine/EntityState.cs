using UnityEngine;

public abstract class EntityState
{
    private string nameState;
    protected StateMachine stateMachine;
    protected Entity entity;

    protected Rigidbody rb;
    private Animator anim;


    public EntityState(StateMachine stateMachine, string nameState, Entity entity)
    {
        this.stateMachine = stateMachine;
        this.nameState = nameState;
        this.entity = entity;

        rb = entity.GetComponent<Rigidbody>();
        anim = entity.GetComponentInChildren<Animator>();
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
