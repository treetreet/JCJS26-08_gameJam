using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 100f;
        [SerializeField] private float m_Health = 100f;

        /// <summary>
        /// Health += amount
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseHealth(float amount)
        {
            m_Health += amount;
            if(m_Health > m_MaxHealth) m_Health = m_MaxHealth;
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
                PlayerDead();    
            }
        }

        public void InvincibleTime(float time)
        {
            StartCoroutine(Invincible(time));
        }

        private IEnumerator Invincible(float time)
        {
            gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
            yield return new WaitForSeconds(time);
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        private void PlayerDead()
        {
            
        }
    }
}