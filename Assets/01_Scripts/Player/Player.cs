using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInput m_PlayerInput;
        private PlayerMovement m_PlayerMovement;
        private PlayerAnimator m_PlayerAnimator;
        private PlayerHealth m_PlayerHealth;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerAnimator = GetComponent<PlayerAnimator>();
            m_PlayerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            // movement
            m_PlayerMovement.Move(m_PlayerInput.InputVector);
            if (m_PlayerInput.JumpInput) m_PlayerMovement.Jump();
            if (m_PlayerInput.DashInput)
            {
                m_PlayerMovement.Dash();
                m_PlayerHealth.InvincibleTime(m_PlayerMovement.DashInvincibleTime);
            }
            
            // animate
            m_PlayerAnimator.Animate(m_PlayerInput.InputVector);
        }
    }
}