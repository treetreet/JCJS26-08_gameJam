using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 m_InputVector;
        private bool m_JumpInput;

        public Vector3 InputVector => m_InputVector;
        public bool JumpInput => m_JumpInput;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            float xInput = 0f;

            if (Keyboard.current.aKey.isPressed)
            {
                xInput--;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                xInput++;
            }

            m_InputVector = new Vector3(xInput, 0f, 0f);

            m_JumpInput = Keyboard.current.wKey.wasPressedThisFrame;
        }
    }
}