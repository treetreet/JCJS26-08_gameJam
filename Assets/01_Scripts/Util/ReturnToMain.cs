using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMain : MonoBehaviour
{
    private float _stopWatch = 20f;

    // Update is called once per frame
    void Update()
    {
        
        _stopWatch -= Time.deltaTime;
        if(_stopWatch <= 0)
            SceneManager.LoadScene(0);

    }
}
