using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private Image m_HealthBar;
        
        [Header("Model")]
        [SerializeField] private PlayerHealth m_PlayerHealth;

        private void Awake()
        {
            m_HealthBar = transform.GetChild(0).gameObject.GetComponent<Image>();
            m_PlayerHealth = transform.root.GetComponentInChildren<PlayerHealth>();

            m_PlayerHealth.OnHealthChanged += UpdateView;
            UpdateView(m_PlayerHealth.Health, m_PlayerHealth.MaxHealth);
        }

        private void UpdateView(float health, float maxHealth)
        {
            m_HealthBar.fillAmount = health / maxHealth;
        }
        
        public void ChangeFillOrigin(float direction)
        {
            if(direction > 0) m_HealthBar.fillOrigin = (int)Image.OriginHorizontal.Right;
            else if (direction < 0) m_HealthBar.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
    }
}