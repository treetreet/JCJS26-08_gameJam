using System.Runtime.CompilerServices;

public abstract class EnemyStateBase
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    public virtual void Enter() {}
    public virtual void Run() {}
    public virtual void Exit() {}
}
