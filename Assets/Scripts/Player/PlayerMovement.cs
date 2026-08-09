using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_MoveSpeed = 5f;
        [SerializeField] private float m_JumpForce = 10f;

        private Rigidbody2D m_Rigidbody;
        private bool m_IsGrounded;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 inputVector)
        {
            m_Rigidbody.linearVelocity = new Vector2(
                inputVector.x * m_MoveSpeed,
                m_Rigidbody.linearVelocity.y
            );
        }

        public void Jump()
        {
            if (!m_IsGrounded)
                return;

            m_Rigidbody.linearVelocity = new Vector2(
                m_Rigidbody.linearVelocity.x,
                m_JumpForce
            );

            m_IsGrounded = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts.Length == 0)
                return;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 바닥과 충돌했는지 확인
                if (contact.normal.y > 0.5f)
                {
                    m_IsGrounded = true;
                    break;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            m_IsGrounded = false;
        }
    }
}