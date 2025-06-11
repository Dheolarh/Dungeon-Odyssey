using UnityEngine;

public class AttackState : IState
{
    private Character _character;
    private StateMachine stateMachine;

    public AttackState(Character player, StateMachine stateMachine)
    {
        this._character = player;
        this.stateMachine = stateMachine;
    }

    public void OnEnterState()
    {
        _character.animator.SetTrigger("OnAttack");
        _character.UpdateAnimatorDirection(_character.facingDirection);
    }


    public void OnUpdateState()
    {
    }
    
 
    public void OnExitState()
    {
        _character.animator.ResetTrigger("OnAttack");
    }
}