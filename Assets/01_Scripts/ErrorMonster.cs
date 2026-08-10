using UnityEngine;

public class ErrorMonster : Enemy
{
    protected override void Patrol()
    {
        DetectHill();
        Move();
    }

    // TODO: Implement the Detect method for ErrorMonster
    protected override void Detect()
    {
        // Implement detect behavior for ErrorMonster
    }

    protected override void Chase()
    {
        Vector2 targetPosition = GameObject.FindWithTag("Player").transform.position;
        Vector2.MoveTowards(transform.position, targetPosition, enemyStat.moveSpeed * Time.deltaTime);
        
        // 플레이어와의 거리가 10 이상일 때 순찰모드로 전환
        if (Vector2.Distance(transform.position, targetPosition) > 10f)
        {
            enemyState = EnemyState.Patrol;
        }
    }

    // TODO: Implement the Attack method for ErrorMonster
    protected override void Attack()
    {
        // Implement attack behavior for ErrorMonster
    }
}
