using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace TempEnemy
{
    public class EnemyAI : MonoBehaviour
    {
        protected IEnemy _enemy;

        [Header("Targets")]
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] protected GameObject _player;
        [Header("Layer Settings")]
        [SerializeField] protected LayerMask _playerLayer; // Inspector에서 Player 레이어 지정 필요

        [Header("Components")]
        [SerializeField] protected Vector2 _moveDir;
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Rigidbody2D _rigid;
        [SerializeField] protected Animator _animator;
        [SerializeField] private AudioSource _audioSource;


        protected int attackHash;
        protected int moveHash;


        private int currentPatrolIndex = 0;

        // 지정된 순찰포인트들을 따라 이동하는 순찰 로직

        internal virtual void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigid = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _enemy = GetComponent<IEnemy>();
            _player = GameObject.FindWithTag("Player");
            attackHash = Animator.StringToHash("Attack");
            moveHash = Animator.StringToHash("IsMove");
            _audioSource = GetComponent<AudioSource>();
        }
        
        internal void FlipSprite()
        {
            _spriteRenderer.flipX = (_moveDir.x == 1) ? true : false;
        }
#region 상태별 행동 로직
        internal virtual void Patrol()
        {
            if (patrolPoints == null || patrolPoints.Count == 0)
            {
                Debug.LogWarning("Patrol points are not assigned.");
                _moveDir = Vector2.zero;
                if(_animator != null)
                    _animator.SetBool(moveHash, false);
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
             // 1. 공격 방향을 플레이어 방향으로 실시간 동기화
            if (_player.transform.position.x > this.transform.position.x)
            {
                _moveDir = Vector2.right;
            }
            else
            {
                _moveDir = Vector2.left;
            }
            FlipSprite(); // 동기화된 방향에 맞춰 스프라이트 회전
            // 2. 공격 상태 진입 가능한 실제 거리 조건 (attackRange 이내일 때 공격 시작)
            float distToPlayer = Mathf.Abs(this.transform.position.x - _player.transform.position.x);
            if (distToPlayer <= _enemy.attackRange)
            {
                if(_animator != null)
                    _animator.SetTrigger(attackHash);

                // 3. OverlapBox 오프셋 조정 (공격 사거리 중간 지점에 생성하는 것이 일반적)
                // 예: 사거리의 절반 만큼 앞에 생성하고 크기를 사거리에 맞추거나, 사거리 끝 지점에 생성하되 범위를 넓힘.
                Vector2 boxCenter = (Vector2)this.transform.position + new Vector2((_enemy.attackRange * 0.5f) * _moveDir.x, 0);
                Vector2 boxSize = new Vector2(_enemy.attackRange, 1f);

                // 4. Player 레이어만 검출하도록 LayerMask 적용하여 자신/바닥 충돌 방지
                Collider2D obj = Physics2D.OverlapBox(boxCenter, boxSize, 0, _playerLayer);
                
                // 5. Null 체크 안전장치 마련
                if (obj != null)
                {
                    if (obj.CompareTag("Player"))
                    {
                        // 플레이어 데미지 코드
                        obj.GetComponent<PlayerHealth>().DecreaseHealth(_enemy.enemyStat.damage);
                        Debug.Log("Player Damage");
                    }
                }
            }
        }

        public void Die()
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0);
            Destroy(this.gameObject);
        }
        #endregion
        
        internal void Move()
        {
            if (_moveDir != Vector2.zero)
            {
                // Y축 속도는 보존하여 중력의 영향을 받도록 처리합니다.
                _rigid.linearVelocity = new Vector2(_moveDir.x * _enemy.moveSpeed, _rigid.linearVelocity.y);
                if(_animator != null)
                    _animator.SetBool(moveHash, true);
                if(_audioSource != null)
                    _audioSource.Play();
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
