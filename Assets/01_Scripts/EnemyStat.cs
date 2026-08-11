using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "Enemy/EnemyStat")]
public class EnemyStat : ScriptableObject
{
    public int Health;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public float moveSpeed;
    public float detectionRange;
}
