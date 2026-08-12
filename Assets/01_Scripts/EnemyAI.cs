using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TempEnemy
{
    public class EnemyAI : MonoBehaviour
    {
        private IEnemy _enemy;

        [Header("Targets")]
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private GameObject _player;

        [Header("Components")]
        [SerializeField] private Vector2 _moveDir;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigid;
        [SerializeField] private Animator _animator;



        private int currentPatrolIndex = 0;

        // 지정된 순찰포인트들을 따라 이동하는 순찰 로직

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigid = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _enemy = GetComponent<IEnemy>();
            _player = GameObject.FindWithTag("Player");
        }
        
        internal void FlipSprite()
        {
            _spriteRenderer.flipX = (_moveDir.x == 1) ? false : true;
        }
#region 상태별 행동 로직
        internal virtual void Patrol()
        {
            if (patrolPoints == null || patrolPoints.Count == 0)
            {
                Debug.LogWarning("Patrol points are not assigned.");
                return;
            }

            Transform targetPoint = patrolPoints[currentPatrolIndex];
            if (targetPoint.position.x > this.transform.position.x)
                _moveDir = Vector2.right;
            else
                _moveDir = Vector2.left;

            Debug.Log($"_moveDir : {_moveDir.x}, {_moveDir.y}");

            if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.1f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                FlipSprite();
            }
            Move();
        }

        internal virtual void Chase()
        {
            // 플레이어 방향으로 이동 방향 설정
            if (_player.transform.position.x > this.transform.position.x)
                _moveDir = Vector2.right;
            else
                _moveDir = Vector2.left;

            FlipSprite();
            Move();
        }

        // TODO : 어택 애니메이션 연결, 플레이어 공격모션에 닿았을 시 체력 깎이는 로직 구현
        internal virtual void Attack()
        {
            
        }

        public IEnumerator Die()
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
        }
        #endregion
        
        internal void Move()
        {
            if (_moveDir != Vector2.zero)
            {
                // Y축 속도는 보존하여 중력의 영향을 받도록 처리합니다.
                _rigid.linearVelocity = new Vector2(_moveDir.x * _enemy.enemyStat.moveSpeed, _rigid.linearVelocity.y);
            }
        }

        internal void DetectHill()
        {
            Vector2 rayOrigin = transform.position + new Vector3(_moveDir.x, 0);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, _groundLayer);

            if (hit.collider == null)
            {
                _moveDir *= -1;

                if (_moveDir.x > 0)
                {
                    _spriteRenderer.flipX = true;
                }
                else if (_moveDir.x < 0)
                {
                    _spriteRenderer.flipX = false;
                }
            }
        }
    }
}
