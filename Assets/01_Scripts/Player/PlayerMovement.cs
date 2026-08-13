using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float m_MoveSpeed = 5f;
        [SerializeField] private float m_MoveAcceleration = 0.1f;
        
        [Header("Jump")]
        [SerializeField] private float m_JumpForce = 10f;
        [SerializeField] private int m_MaxJumpCount = 2;
        
        [Header("Dash")]
        [SerializeField] private float m_DashForce = 8f;
        [SerializeField] private float m_DashCooldown = 3f;
        [SerializeField] private float m_DashInvincibleTime = 0.3f;
        
        private Rigidbody2D m_Rigidbody;
        private int m_JumpCount;
        private bool m_IsDashCool;

        public float DashInvincibleTime => m_DashInvincibleTime;
        
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 inputVector)
        {
            float targetSpeed = inputVector.x * m_MoveSpeed;

            float currentSpeed = m_Rigidbody.linearVelocityX;

            if (Mathf.Abs(targetSpeed) < Mathf.Abs(currentSpeed) && targetSpeed * currentSpeed > 0f) return;

            float newSpeed = Mathf.MoveTowards(
                currentSpeed,
                targetSpeed,
                m_MoveAcceleration * Time.fixedDeltaTime
            );

            m_Rigidbody.linearVelocity = new Vector2(
                newSpeed,
                m_Rigidbody.linearVelocity.y
            );
        }
        
        public void Jump()
        {
            if (m_JumpCount >= m_MaxJumpCount)
                return;

            m_Rigidbody.linearVelocity = new Vector2(
                m_Rigidbody.linearVelocity.x,
                m_JumpForce
            );

            m_JumpCount++;
        }

        public void Dash()
        {
            if (m_IsDashCool || m_Rigidbody.linearVelocity == Vector2.zero) return;

            m_IsDashCool = true;
            // 대시 기능
            int direction = m_Rigidbody.linearVelocity.x > 0
                ? 1
                : -1;

            m_Rigidbody.AddForceX(m_DashForce * direction, ForceMode2D.Impulse);

            StartCoroutine(DashCooldown());
        }

        private IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(m_DashCooldown);
            
            m_IsDashCool =  false;
        }

        // 점프 초기화
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts.Length == 0)
                return;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 바닥과 충돌했는지 확인
                if (contact.normal.y > 0.5f)
                {
                    m_JumpCount = 0;
                    break;
                }
            }
        }
    }
}