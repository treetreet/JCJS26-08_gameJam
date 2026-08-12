using System.Collections.Generic;
using UnityEngine;


    public interface IEnemy
    {
        public EnemyStat enemyStat { get; }
    }
    
    public interface IDamageable
    {
        void Damaged(int damage);
    }

namespace TempEnemy
{


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
        Hear,
        Boss
    }

    public class Enemy : MonoBehaviour, IDamageable, IEnemy
    {
        [Header("Enemy Stat")]
        [field: SerializeField] public EnemyStat enemyStat { get; private set; }
        [SerializeField] public EnemyType enemyType;
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
            // 공격 범위 내에 들어오면 공격 상태로 전환
            if (distanceToPlayer <= enemyStat.attackRange)
            {
                enemyState = EnemyState.Attack;
                return;
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
            enemyStat.Health -= damage;
            if (enemyStat.Health <= 0)
            {
                StartCoroutine(_enemyAI.Die());
            }
        }

        protected void SetDetectRange()
        {
            switch (enemyType)
            {
                case EnemyType.Bat:
                    if(GimmickManager.instance.m_LightSlider.value > 0.2f)
                        enemyStat.detectionRange = 0;
                break;
                case EnemyType.Error:
                    enemyStat.detectionRange = GimmickManager.instance.m_LightSlider.value * 10f;
                    Debug.Log(enemyStat.detectionRange);
                break;
                case EnemyType.Hear:
                    enemyStat.detectionRange = GimmickManager.instance.m_SoundSlider.value * 30f;
                break;
            }
        }

    }
}