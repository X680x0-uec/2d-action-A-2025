
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    [Header("1キーで遷移するシーン名")]
    public string sceneOnAlpha1 = "AnotherScene";

    private bool isTransitioning = false; // 多重遷移防止

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // エディタ上で停止
        #else
            Application.Quit(); // ビルド版で終了
        #endif
    }

    void Update()
    {
        // 数字の1キー（上段 or テンキー）でシーン遷移
        if (!isTransitioning && (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)))
        {
            Transition(sceneOnAlpha1);
        }
    }

    private void Transition(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[ExitGame] 遷移先シーン名が設定されていません。");
            return;
        }

        isTransitioning = true;
        SceneManager.LoadScene(sceneName);
    }
}
