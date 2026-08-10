using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    Patrol,
    Detect,
    Chase,
    Attack,
}

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stat")]
    public EnemyStat enemyStat;
    private EnemyState enemyState;

    [Header("Enemy Components")]
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Enemy Settings")]
    [SerializeField] private Vector2 _moveDir;
    [SerializeField] private LayerMask _groundLayer;


    void Start()
    {
        enemyState = EnemyState.Patrol;
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveDir = Vector2.left;
        _spriteRenderer.flipX = _moveDir.x > 0;
    }

    void Update()
    {
        Debug.Log($"Current Enemy State: {enemyState}");
        switch (enemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Detect:
                Detect();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    protected void Move()
    {
        if (_moveDir != Vector2.zero)
        {
            _rigid.linearVelocity = _moveDir * enemyStat.moveSpeed;
        }
    }

    protected void DetectHill()
    {
        Vector2 rayOrigin = transform.position + new Vector3(_moveDir.x, 0);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, _groundLayer);

        if (hit.collider == null)
        {
            _moveDir *= -1;

            if(_moveDir.x > 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if(_moveDir.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    protected virtual void Patrol()
    {
    }

    protected virtual void Detect()
    {
        // Implement detect behavior
    }

    protected virtual void Chase()
    {
        // Implement chase behavior
    }

    protected virtual void Attack()
    {
        // Implement attack behavior
    }
}
