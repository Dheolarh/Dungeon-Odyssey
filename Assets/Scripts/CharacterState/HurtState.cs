using UnityEngine;
public class HurtState : IState
{
    private Character player;
    private StateMachine stateMachine;

    public HurtState(Character player, StateMachine stateMachine)
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