using System;
using UnityEngine;

namespace Sword
{
    public class SwordAttack : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 30;
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Collision Enter : " + col.gameObject.name);
            if(col.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(col.gameObject.name + "Damaged");
                col.gameObject.GetComponent<IDamageable>().Damaged(m_Damage);
            }
        }
    }
}