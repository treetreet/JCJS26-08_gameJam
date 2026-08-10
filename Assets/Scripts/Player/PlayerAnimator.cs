using UnityEngine;

namespace Player
{
    public class PlayerAnimator :MonoBehaviour
    {
        private Animator m_Animator;
        private SpriteRenderer m_SpriteRenderer;
        private Rigidbody2D m_Rigidbody;

        private static string k_WalkState = "isWalking";
        private static string k_IdleState = "isIdle";
        private static string k_JumpState = "isJumping";
        private static string k_FallState = "isFalling";
        
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Animate(Vector3 inputVector)
        {
            m_Animator.SetBool(k_IdleState, inputVector == Vector3.zero && m_Rigidbody.linearVelocity == Vector2.zero);
            m_Animator.SetBool(k_WalkState, inputVector != Vector3.zero);
            m_Animator.SetBool(k_JumpState, m_Rigidbody.linearVelocity.y > 0);
            m_Animator.SetBool(k_FallState, m_Rigidbody.linearVelocity.y < 0);
            
            if(inputVector.x > 0)           m_SpriteRenderer.flipX = true;
            else if(inputVector.x < 0)      m_SpriteRenderer.flipX = false;
        }
    }
}