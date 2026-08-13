using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_Scripts
{
    public class GameStartButton : MonoBehaviour
    {
        public void GameStart()
        {
            SceneManager.LoadScene("Scenes/Tutorial");
        }
    }
}