using UnityEngine;
namespace TempEnemy
{
    public class InvincibleAI : EnemyAI
    {

        internal override void Patrol()
        {
             base.Patrol();
        }

        internal override void Chase()
        {
            base.Chase();
        }
        
        internal override void Attack()
        {
           base.Attack();
        }
    }
}