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

    public override void Active(Player player, out bool successActive)
    {
        base.Active(player, out successActive);
        followerMovement.AddFollowTarget(player.transform);
    }

    protected override void OnPauseGame()
    {
        base.OnPauseGame();
        followerMovement.EnableMovement(false);
    }

    protected override void OnResumeGame()
    {
        base.OnResumeGame();
        followerMovement.EnableMovement(true);
    }

    public bool HaveSaltJar() => false; // Npc can't hold salt jar
    public void DismissSaltJar() { } // Npc can't hold salt jar
}
