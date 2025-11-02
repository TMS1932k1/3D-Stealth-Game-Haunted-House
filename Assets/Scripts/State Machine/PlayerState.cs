using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;


    public PlayerState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
        this.player = player;
    }
}
