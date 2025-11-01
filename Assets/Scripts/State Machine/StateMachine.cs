using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PlayerState currentState { get; private set; }


    public void InitializeState(PlayerState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }
}
