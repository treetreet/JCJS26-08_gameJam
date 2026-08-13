using System;
using System.Collections;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bat;
    [SerializeField] private float _coolTime;

    void Start()
    {
        StartCoroutine(CreateRobotBat());
    }

    IEnumerator CreateRobotBat()
    {
        while (true)
        {
            if(GimmickManager.instance.m_LightSlider.value <= 0.2f)
            {
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(_bat, transform.position, transform.rotation);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(_coolTime);
        }
    }
}
