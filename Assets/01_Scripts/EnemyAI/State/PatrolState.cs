using UnityEngine;

public class PatrolState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Run()
    {
        
        if(enemy.DistanceToPlayer() <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.m_AttackState);
        }
        else
        {
            stateMachine.ChangeState(enemy.m_ChaseState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}