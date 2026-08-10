using System;
using UnityEngine;

namespace TempEnemy
{
    public class Enemy : MonoBehaviour
    {
        private EnemyVFX m_EnemyVFX;

        private void Awake()
        {
            m_EnemyVFX = GetComponent<EnemyVFX>();
        }

        public void Damaged()
        {
            m_EnemyVFX.DamagedEffect();
        }
        private void Update()
        {
            
        }
    }
}