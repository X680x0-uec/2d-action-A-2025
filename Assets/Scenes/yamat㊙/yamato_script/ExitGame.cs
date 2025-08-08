
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // エディタ上で停止
        #else
            Application.Quit(); // ビルド版で終了
        #endif
    }
}
