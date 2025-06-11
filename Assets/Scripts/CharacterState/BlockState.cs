using UnityEngine;
public class BlockState : IState
{
    private Character _character;
    private StateMachine _stateMachine;

    public BlockState(Character player, StateMachine stateMachine)
    {
        this._character = player;
        this._stateMachine = stateMachine;
    }
    public void OnEnterState()
    {
        _character.animator.SetTrigger("OnBlock");
    }

    public void OnUpdateState()
    {
    }

    public void OnExitState()
    {
        _character.animator.ResetTrigger("OnBlock");
        _character.animator.SetBool("IsBlocking", false);
    }
}