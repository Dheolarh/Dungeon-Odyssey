using UnityEngine;
public class IdleState : IState
{
    private Character _character;
    private StateMachine _stateMachine;

    public IdleState(Character player, StateMachine stateMachine)
    {
        this._character = player;
        this._stateMachine = stateMachine;
    }
    // C#
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
 
