using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 100f;
        [SerializeField] private float m_Health = 100f;

        public float MaxHealth => m_MaxHealth;
        public float Health => m_Health;
        
        /// <summary>
        ///Health, MaxHealth
        /// </summary>
        public event Action<float, float> OnHealthChanged;

        [ContextMenu("IncreaseHealth10")]
        public void IncreaseHealth10()
        {
            IncreaseHealth(10);
        }
        
        [ContextMenu("DecreaseHealth 10")]
        public void DecreaseHealth10()
        {
            DecreaseHealth(10);
        }
        
        
        /// <summary>
        /// Health += amount
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseHealth(float amount)
        {
            m_Health += amount;
            if(m_Health > m_MaxHealth) m_Health = m_MaxHealth;
            
            OnHealthChanged?.Invoke(m_Health, m_MaxHealth);
        }

        /// <summary>
        /// Health -= amount
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseHealth(float amount)
        {
            m_Health -= amount;
            if (m_Health < 0)
            {
                m_Health = 0;
                OnHealthChanged?.Invoke(m_Health, m_MaxHealth);
                
                PlayerDead();    
            }
            
            OnHealthChanged?.Invoke(m_Health, m_MaxHealth);

            InvincibleTime(0.2f);
        }

        public void InvincibleTime(float time)
        {
            StartCoroutine(Invincible(time));
        }

        private IEnumerator Invincible(float time)
        {
            gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
            yield return new WaitForSeconds(time);
            gameObject.layer = LayerMask.NameToLayer("PLAYER");
        }

        private void PlayerDead()
        {
            
        }
    }
}