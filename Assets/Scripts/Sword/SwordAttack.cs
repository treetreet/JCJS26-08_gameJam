using System;
using UnityEngine;

namespace Sword
{
    public class SwordAttack : MonoBehaviour
    {
        private CapsuleCollider2D m_Collider2D;
        
        private void Awake()
        {
            m_Collider2D = GetComponent<CapsuleCollider2D>();
            m_Collider2D.isTrigger = true;
            m_Collider2D.enabled = false;
        }

        // animation event
        private void ColliderEnableChange(int value)
        {
            m_Collider2D.enabled = value == 1;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<IDamageable>().Damaged(10);
            }
        }
    }
}