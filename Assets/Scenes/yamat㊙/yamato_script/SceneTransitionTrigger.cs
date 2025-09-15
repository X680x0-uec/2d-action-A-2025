using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string nextSceneName = "MiniGameScene"; // 遷移先のシーン名

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("walker")) // プレイヤーのTagが"walker"なら
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
