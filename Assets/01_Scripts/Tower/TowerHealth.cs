using UnityEngine;

public class TowerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _towerHP = 100f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Beam"))
        {
            Beam beam = col.gameObject.GetComponent<Beam>();
            if (beam != null && beam.hasReflected)
            {
                _towerHP -= 5;
                Destroy(col.gameObject);
                if (_towerHP <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beam"))
        {
            Beam beam = other.gameObject.GetComponent<Beam>();
            if (beam != null && beam.hasReflected)
            {
                _towerHP -= 5;
                Destroy(other.gameObject);
                if (_towerHP <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Damaged(int damage)
    {
        Damaged((float)damage);
    }

    public void Damaged(float damage)
    {
        _towerHP -= damage;
        if(_towerHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
