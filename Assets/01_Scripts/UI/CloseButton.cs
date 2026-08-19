using UnityEditor;
using UnityEngine;


namespace UI_Scripts
{
    public class CloseButton : MonoBehaviour
    {
        public void QuitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}