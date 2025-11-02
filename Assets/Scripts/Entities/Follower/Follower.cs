using UnityEngine;

public class Follower : Npc, IGetCaught
{
    public Follower_Movement followerMovement { get; private set; }

    public Follower_IdleState idleState { get; private set; }
    public Follower_WalkState walkState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        followerMovement = GetComponent<Follower_Movement>();

        idleState = new Follower_IdleState(stateMachine, PlayerAnimationStrings.IDLE_ANIM, this);
        walkState = new Follower_WalkState(stateMachine, PlayerAnimationStrings.WALK_ANIM, this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.InitializeState(idleState);
    }

    public void DisableInputManager()
    {
        followerMovement.DisableFollowMovement();
    }

    public override void Active(Player player)
    {
        base.Active(player);

        followerMovement.AddFollowTarget(player.transform);
    }
}
