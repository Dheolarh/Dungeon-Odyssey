using UnityEngine;
public class RunState : IState, IPhysicsState
{
    private Character _character;
    private StateMachine stateMachine;
    

    public RunState(Character player, StateMachine stateMachine, float moveSpeed)
    {
        this._character = player;
        this.stateMachine = stateMachine;
        this._character.moveSpeed = moveSpeed;
    }
    public void OnEnterState()
    {
        
    }
    public void OnUpdateState()
    {
    
    }

  

    public void OnExitState()
    {
        
        
    }

    public void FixedUpdate()
    {
        if (_character.moveInput != Vector2.zero)
        {
            Vector2 movement = _character.moveInput * _character.moveSpeed * Time.fixedDeltaTime;
            _character.transform.position += (Vector3)movement;
        }
    }
}