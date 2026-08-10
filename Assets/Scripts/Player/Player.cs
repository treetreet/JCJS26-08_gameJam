using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInput m_PlayerInput;
        private PlayerMovement m_PlayerMovement;
        private PlayerAnimator m_PlayerAnimator;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerAnimator = GetComponent<PlayerAnimator>();
        }

        private void Update()
        {
            // movement
            m_PlayerMovement.Move(m_PlayerInput.InputVector);
            if (m_PlayerInput.JumpInput) m_PlayerMovement.Jump();
            
            // animate
            m_PlayerAnimator.Animate(m_PlayerInput.InputVector);
        }
    }
}