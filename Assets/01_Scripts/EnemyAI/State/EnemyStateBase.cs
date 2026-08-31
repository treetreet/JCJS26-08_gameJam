using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class EnemyStateBase
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected GameObject m_Player;
    public virtual void Enter() {}
    
    /// <summary>
    /// 하위 상태 클래스들은 여기에 다른 상태로의 트랜지션 조건 체크를 적어둘 것.
    /// </summary>
    public virtual void Run() {}    
    public virtual void Exit() {}
}
