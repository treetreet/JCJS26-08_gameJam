using System.Collections.Generic;
using UnityEngine;


namespace TempEnemy
{
    public interface IDamageable
    {
        void Damaged();
    }

    public interface IEnemy
    {
        public EnemyStat enemyStat { get; }
    }

    public enum EnemyState
    {
        Patrol,
        Detect,
        Chase,
        Attack,
        Die
    }

    public class Enemy : MonoBehaviour, IDamageable, IEnemy
    {
        [Header("Enemy Stat")]
        [field: SerializeField] public EnemyStat enemyStat { get; private set; }
        protected EnemyState enemyState;

        [Header("Enemy Components")]
        [SerializeField] private EnemyVFX m_EnemyVFX;
        [SerializeField] private GameObject _player;




        [Header("Enemy Settings")]
        private EnemyAI _enemyAI;

        void Start()
        {
            enemyState = EnemyState.Patrol;
            m_EnemyVFX = GetComponent<EnemyVFX>();
            _enemyAI = GetComponent<EnemyAI>();
            _player = GameObject.FindWithTag("Player");
        }

       
        public virtual void ChangeState()
        {
            if(enemyState == EnemyState.Die)
                return;

            float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

            // 공격 범위 내에 들어오면 공격 상태로 전환
            if (distanceToPlayer <= enemyStat.attackRange)
            {
                enemyState = EnemyState.Attack;
                return;
            }

            // 감지 범위를 벗어나면 다시 순찰 상태로 복귀
            if (_player == null || distanceToPlayer > enemyStat.detectionRange)
            {

                enemyState = EnemyState.Patrol;
                return;
            }
            else
            {
                enemyState = EnemyState.Chase;
            }
        }

        void Update()
        {
            Debug.Log($"Current Enemy State: {enemyState}");
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

        public void Damaged()
        {
            m_EnemyVFX.DamagedEffect();
            // TODO : 플레이어의 공격력 받아와 체력 감소 로직
            if (enemyStat.Health <= 0)
            {
                StartCoroutine(_enemyAI.Die());
            }
        }

    }
}