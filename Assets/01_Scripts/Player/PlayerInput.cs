using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector3 m_InputVector;
        private bool m_JumpInput;
        private bool m_DashInput;

        public Vector3 InputVector => m_InputVector;
        public bool JumpInput => m_JumpInput;
        public bool DashInput => m_DashInput;
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

            m_JumpInput = Keyboard.current.wKey.wasPressedThisFrame ||  Keyboard.current.spaceKey.wasPressedThisFrame;
            m_DashInput = Keyboard.current.shiftKey.wasPressedThisFrame;
        }
    }
}