using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyStateBase currentState;

    public void Init(EnemyStateBase stateBase)
    {
        currentState = stateBase;
        currentState!.Enter();   
    }

    public void ChangeState(EnemyStateBase newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
