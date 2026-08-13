using UnityEngine;

public class StageThree : MonoBehaviour
{
    [SerializeField] GameObject _tower;

    private float three = 3f;

    void Update()
    {
        three -= Time.deltaTime;
        if (three <= 0)
        {
            _tower.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
