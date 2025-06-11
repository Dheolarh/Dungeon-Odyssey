using UnityEngine;

public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState();
    }

    public void Update()
    {
        currentState?.OnUpdateState();
    }
    
    public void FixedUpdate()
    {
        if (currentState is IPhysicsState physicsState)
            physicsState.FixedUpdate();
    }
}
