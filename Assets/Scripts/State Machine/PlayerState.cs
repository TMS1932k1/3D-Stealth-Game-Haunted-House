using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;
    protected Player_InputManager inputManager;


    public PlayerState(StateMachine stateMachine, string nameState, Player player) : base(stateMachine, nameState, player)
    {
        this.player = player;

        inputManager = player.inputManager;
    }
}
