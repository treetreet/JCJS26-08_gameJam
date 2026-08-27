using System.Collections.Generic;
using UnityEngine;
using TempEnemy;


    public interface IEnemy
    {
        public int Health { get; set; }
        public int damage { get; }
        public float attackSpeed { get; }
        public float attackRange { get; }
        public float moveSpeed { get; }
        public float detectionRange { get; }
    }
    
    public interface IDamageable
    {
        void Damaged(int damage);
    }

    public enum EnemyState
    {
        Patrol,
        Detect,
        Chase,
        Attack,
        Die
    }

    public enum EnemyType
    {
        Bat,
        Error,
        Invincible,
        Hear,
        Boss
    }

    public class Enemy : MonoBehaviour, IDamageable, IEnemy
    {
        [Header("Enemy Stat Template")]
        [SerializeField] private EnemyStat enemyStatTemplate;
        [SerializeField] public EnemyType enemyType;
        protected EnemyState enemyState;

        private float _currentDetectionRange;

        [Header("Enemy Components")]
        [SerializeField] private EnemyVFX m_EnemyVFX;
        [SerializeField] private GameObject _player;

        [Header("Enemy Settings")]
        private EnemyAI _enemyAI;

        // 분리된 런타임 스탯 필드들
        [Header("Live Stats")]
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _detectionRange;

        // IEnemy 인터페이스 구현
        public int Health { get => _health; set => _health = value; }
        public int damage => _damage;
        public float attackSpeed => _attackSpeed;
        public float attackRange => _attackRange;
        public float moveSpeed => _moveSpeed;
        public float detectionRange => _detectionRange;

        void Awake()
        {
            if (enemyStatTemplate != null)
            {
                // SO의 값으로 스탯 초기화
                _health = enemyStatTemplate.Health;
                _damage = enemyStatTemplate.damage;
                _attackSpeed = enemyStatTemplate.attackSpeed;
                _attackRange = enemyStatTemplate.attackRange;
                _moveSpeed = enemyStatTemplate.moveSpeed;
                _detectionRange = enemyStatTemplate.detectionRange;
            }
            else
            {
                Debug.LogWarning($"EnemyStatTemplate is not assigned on {gameObject.name}");
            }
        }

        void Start()
        {
            enemyState = EnemyState.Patrol;
            m_EnemyVFX = GetComponent<EnemyVFX>();
            _enemyAI = GetComponent<EnemyAI>();
            _player = GameObject.FindWithTag("Player");
            
            _currentDetectionRange = detectionRange;
        }

       
        public virtual void ChangeState()
        {
            if(enemyState == EnemyState.Die)
                return;

            if (_player == null)
            {
                enemyState = EnemyState.Patrol;
                return;
            }

            float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

            // 감지 범위를 벗어나면 다시 순찰 상태로 복귀
            if (distanceToPlayer > _currentDetectionRange)
            {
                enemyState = EnemyState.Patrol;
            }
            else
            {
                // 감지 범위 내에 들어왔을 때만 공격 범위 판정
                if (distanceToPlayer <= attackRange)
                {
                    enemyState = EnemyState.Attack;
                }
                else
                {
                    enemyState = EnemyState.Chase;
                }
            }
        }

        void Update()
        {
            Debug.Log($"Current Enemy State: {enemyState}");
            SetDetectRange();
            ChangeState();
            switch (enemyState)
            {
                case EnemyState.Die:
                    break;
                case EnemyState.Patrol:
                    _enemyAI.Patrol();
                    break;
                case EnemyState.Chase:
                    _enemyAI.Chase();
                    break;
                case EnemyState.Attack:
                    _enemyAI.Attack();
                    break;
            }
        }

        public void Damaged(int damage)
        {
            m_EnemyVFX.DamagedEffect();
            Health -= damage;
            if (Health <= 0)
            {
                _enemyAI.Die();
            }
        }

        protected void SetDetectRange()
        {
            if (enemyStatTemplate == null) return;

            switch (enemyType)
            {
                case EnemyType.Bat:
                    if (GimmickManager.instance != null && GimmickManager.instance.m_LightSlider.value > 0.2f)
                        _currentDetectionRange = 0;
                    else
                        _currentDetectionRange = detectionRange;
                break;
                case EnemyType.Error:
                    if (GimmickManager.instance != null)
                        _currentDetectionRange = GimmickManager.instance.m_LightSlider.value * 10f;
                break;
                case EnemyType.Invincible:
                    _currentDetectionRange = 20;
                break;
                case EnemyType.Hear:
                    if (GimmickManager.instance != null)
                        _currentDetectionRange = GimmickManager.instance.m_SoundSlider.value + 40;
                break;
            }
        }

    }