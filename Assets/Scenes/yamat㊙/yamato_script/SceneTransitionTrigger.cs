using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string nextSceneName = "MiniGameScene"; // 遷移先のシーン名

    private bool isTransitioning = false; // 多重遷移防止

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransitioning && other.CompareTag("walker")) // プレイヤーのTagが"walker"なら
        {
            Transition();
        }
    }

    private void Update()
    {
        // バックスペースキーでシーン遷移
        if (!isTransitioning && Input.GetKeyDown(KeyCode.Backspace))
        {
            Transition();
        }
    }

    private void Transition()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Scene name is not set.");
            return;
        }

        isTransitioning = true;
        SceneManager.LoadScene(nextSceneName);
    }
}