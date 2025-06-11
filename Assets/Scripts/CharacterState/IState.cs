public interface IState
{
      void OnEnterState();
      void OnUpdateState();
      void OnExitState();
}

public interface IPhysicsState
{
      void FixedUpdate();
}