using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("時間表示")]
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI remainingTimeText;
    public float timeScale = 1f;

    private System.DateTime gameStartTime;
    private float elapsedRealTime = 0f;
    private float totalGameSeconds = 600f; // 10分

    [Header("スコア表示")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        gameStartTime = new System.DateTime(2025, 1, 1, 8, 50, 0);
        UpdateScoreText();
    }

    void Update()
    {
        UpdateClock();
    }

    void UpdateClock()
    {
        if (elapsedRealTime * timeScale < totalGameSeconds)
        {
            elapsedRealTime += Time.deltaTime;
            float gameElapsedSeconds = elapsedRealTime * timeScale;

            System.DateTime currentGameTime = gameStartTime.AddSeconds(gameElapsedSeconds);
            currentTimeText.text = currentGameTime.ToString("HH:mm:ss");

            float remaining = totalGameSeconds - gameElapsedSeconds;
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            remainingTimeText.text = $"遅刻まで: {minutes:D2}:{seconds:D2}";
        }
        else
        {
            currentTimeText.text = "09:00:00";
            remainingTimeText.text = "ちこく～～";
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = $"score: {score}";
    }
}
