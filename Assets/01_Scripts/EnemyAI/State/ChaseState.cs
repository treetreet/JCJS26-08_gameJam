
public class ChaseState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Run()
    {
        if(enemy.DistanceToPlayer() > enemy._currentDetectionRange)
            stateMachine.ChangeState(enemy.m_PatrolState);
        
        if(enemy.DistanceToPlayer() <= enemy.attackRange)
            stateMachine.ChangeState(enemy.m_AttackState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}