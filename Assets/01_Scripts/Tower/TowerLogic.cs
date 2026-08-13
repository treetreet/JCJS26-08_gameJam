using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TowerLogic : MonoBehaviour
{
    [SerializeField] private GameObject _beam;
    [SerializeField] private AudioSource _audioSource;


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(GenerateBeam());
    }

    private IEnumerator GenerateBeam()
    {
        while (true)
        {
            for (float i = 0; i < 5; i += 0.1f)
            {
                Instantiate(_beam, transform.position, quaternion.Euler(0, 0, Random.Range(-45f, 0f)));
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void OnDestroy()
    {
        _audioSource.Play();
        SceneManager.LoadScene(4);
    }
}
