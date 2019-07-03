using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    private State previousState;

    private Animator animator;

    public void ChangeState(State nextState)
    {
        if (currentState != null) currentState.Exit();

        previousState = currentState;
        currentState = nextState;

        currentState.Enter();
    }

    public void ChangeState(State nextState, string animation, bool trigger)
    {
        if (currentState != null) currentState.Exit();

        previousState = currentState;
        currentState = nextState;

        if (trigger) animator.SetTrigger(animation);
        else animator.SetBool(animation, true);

        currentState.Enter();
    }

    public void ExecuteState()
    {
        if (currentState != null) currentState.Execute();
    }

    public void BackToPrevoius()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}

