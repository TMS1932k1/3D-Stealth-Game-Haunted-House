using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; private set; }


    public void InitializeState(EntityState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    public void ChangeState(EntityState newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }
}
