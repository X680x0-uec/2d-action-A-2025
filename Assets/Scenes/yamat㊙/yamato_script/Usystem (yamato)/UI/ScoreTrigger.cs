using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameUIManager gameUIManager; // GameUIManager をインスペクターで接続
    public int scoreAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameUIManager.AddScore(scoreAmount);

            // 一度だけ加算したい場合はオブジェクトを消す
            // Destroy(gameObject);
        }
    }
}
