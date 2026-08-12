using System.Collections.Generic;
using TempEnemy;
using UnityEngine;

public class HearMonsterAI : EnemyAI
{
    [SerializeField] private List<SpriteRenderer> m_EarSprites;

    internal override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    internal override void Patrol()
    {
        foreach(SpriteRenderer spriteRenderer in m_EarSprites)
        {
            spriteRenderer.enabled = false;
        }
        base.Patrol();
    }

    internal override void Chase()
    {
        if(_player.transform.position.x > this.transform.position.x)
        {
            m_EarSprites[0].enabled = true;
            m_EarSprites[1].enabled = false;
        }
        else if(_player.transform.position.x < this.transform.position.x)
        {
            m_EarSprites[1].enabled = true;
            m_EarSprites[0].enabled = false;
        }
        if(_player.transform.position.y > this.transform.position.y)
        {
            m_EarSprites[2].enabled = true;
        }
        base.Chase();
    }

    internal override void Attack()
    {
        base.Attack();
    }
}
