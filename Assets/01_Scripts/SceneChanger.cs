using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string m_SceneName;
    [SerializeField] private Image fadeOut;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        while (fadeOut.color.a < 1)
        {
            fadeOut.color =  new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, fadeOut.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        
        SceneManager.LoadScene(m_SceneName);
    }
}
