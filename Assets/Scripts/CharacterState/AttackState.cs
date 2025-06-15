using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackState : IState
{
    private Character _character;
    private StateMachine _stateMachine;
    private Enemy _enemy;

    public AttackState(Character player, StateMachine stateMachine)
    {
        this._character = player;
        this._stateMachine = stateMachine;
    }

    public void OnEnterState()
    {
        _character.isAttacking = true;
        _character.animator.SetTrigger("OnAttack");
        _character.UpdateAnimatorDirection(_character.facingDirection);
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            Enemy currentEnemy = Collidables.Instance.GetCurrentEnemy();
            if(currentEnemy != null)
            {
                currentEnemy.TakeDamage(3);
            }
        }
        _character.Invoke(nameof(_character.SwitchToIdleState), _character.attackDuration);
    }

    


    public void OnUpdateState()
    {
    }

    
    public static bool IsActive(StateMachine stateMachine)
    {
        return stateMachine.currentState is AttackState;
    }
 
    public void OnExitState()
    {
       
    }
}