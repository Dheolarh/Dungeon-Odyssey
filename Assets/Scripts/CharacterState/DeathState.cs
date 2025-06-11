using UnityEngine;
public class DeathState : IState
{
    private Character player;
    private StateMachine stateMachine;

    public DeathState(Character player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    public void OnEnterState()
    {
        
    }

    public void OnUpdateState()
    {
        
    }

    public void OnExitState()
    {
        Debug.Log("Exiting IdleState State");
    }
}