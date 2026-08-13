using TempEnemy;
using UnityEngine;

public class RobotBatAI : EnemyAI
{
    private Camera _mainCamera;

    private Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

    internal override void Patrol()
    {
        if (GimmickManager.instance == null || _player == null) return;

        // 중력 강제 비활성화 (미끄러짐 및 처짐 현상 방지)
        if (_rigid.gravityScale != 0f)
        {
            _rigid.gravityScale = 0f;
        }

        float brightness = GimmickManager.instance.m_LightSlider.value;

        if (brightness >= 0.2f)
        {
            // 1. 밝기가 20 이상일 때는 뷰포트의 최상단에 머물러 있는다.
            if (MainCamera != null)
            {
                // 현재 월드 좌표를 뷰포트 좌표로 변환
                Vector3 viewportPos = MainCamera.WorldToViewportPoint(_rigid.position);
                
                // 화면 가로 밖으로 나가지 않도록 범위를 5%~95% 사이로 클램프
                float targetViewportX = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
                
                // viewportPos.z를 동적으로 사용하여 카메라와의 실제 거리를 오차 없이 반영
                Vector3 targetViewportPos = new Vector3(targetViewportX, 0.9f, viewportPos.z);
                Vector3 targetWorldPos = MainCamera.ViewportToWorldPoint(targetViewportPos);
                
                // 목표지점과의 거리가 0.05f보다 클 때만 이동 (도달 시 미세 떨림/Jitter 방지)
                if (Vector2.Distance(_rigid.position, targetWorldPos) > 0.05f)
                {
                    // Rigidbody2D를 사용하여 물리 법칙을 준수하며 부드럽게 이동
                    Vector2 nextPos = Vector2.MoveTowards(_rigid.position, targetWorldPos, _enemy.enemyStat.moveSpeed * Time.deltaTime);
                    _rigid.MovePosition(nextPos);
                }
            }

            // 머물러 있을 때는 이동 애니메이션을 끄고 속도를 제로화
            _rigid.linearVelocity = Vector2.zero;
        }
        else
        {
            // 2. 밝기가 20 미만으로 떨어지면 플레이어를 쫓아온다.
            ChasePlayer();
        }
    }

    internal override void Chase()
    {
        if (GimmickManager.instance == null || _player == null) return;

        float brightness = GimmickManager.instance.m_LightSlider.value;

        if (brightness >= 0.2f)
        {
            // 밝기가 20 이상이면 추적을 멈추고 최상단으로 복귀 (Patrol 행동 수행)
            Patrol();
        }
        else
        {
            // 밝기가 20 미만이면 플레이어 추격
            ChasePlayer();
        }
    }

    internal override void Attack()
    {
        if (GimmickManager.instance == null || _player == null) return;

        float brightness = GimmickManager.instance.m_LightSlider.value;

        if (brightness >= 0.2f)
        {
            // 밝기가 20 이상이면 공격을 멈추고 최상단으로 복귀 (Patrol 행동 수행)
            Patrol();
        }
        else
        {
            // 밝기가 20 미만이면 기본 공격 수행
            base.Attack();
        }
    }

    private void ChasePlayer()
    {
        Vector2 targetPos = _player.transform.position;
        
        // 2D 비행 몬스터이므로 X, Y축 모두 플레이어를 향해 직접 이동
        Vector2 nextPos = Vector2.MoveTowards(_rigid.position, targetPos, _enemy.enemyStat.moveSpeed * Time.deltaTime);
        _rigid.MovePosition(nextPos);

        // Rigidbody 벨로시티를 0으로 유지하여 MovePosition이 원활히 동작하도록 함
        _rigid.linearVelocity = Vector2.zero;

        // 플레이어의 방향에 맞춰 _moveDir 및 스프라이트 방향 설정
        if (targetPos.x > _rigid.position.x)
            _moveDir = Vector2.right;
        else
            _moveDir = Vector2.left;

        FlipSprite();
        _animator.SetBool(moveHash, true);
    }
}
