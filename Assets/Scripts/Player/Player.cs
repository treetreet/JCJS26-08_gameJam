using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInput m_PlayerInput;
        private PlayerMovement m_PlayerMovement;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            m_PlayerMovement.Move(m_PlayerInput.InputVector);

            if (m_PlayerInput.JumpInput)
            {
                m_PlayerMovement.Jump();
            }
        }
    }
}