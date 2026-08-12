using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] private float _towerHP = 100f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Beam" && col.gameObject.GetComponent<Beam>().hasReflected)
        {
            _towerHP -= 5;
            if(_towerHP <= 0)
            {
                Destroy(gameObject);
            }
        }
        
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
