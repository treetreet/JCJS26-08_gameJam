using UnityEngine;
using UnityEngine.InputSystem;

namespace Sword
{
    public class SwordInput : MonoBehaviour
    {
        private bool m_ClickInput;
        public bool ClickInput => m_ClickInput;
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            m_ClickInput = Mouse.current.leftButton.wasPressedThisFrame;
        }
    }
}