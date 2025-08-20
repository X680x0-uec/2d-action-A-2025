using UnityEngine;

public class WetnessCounter : MonoBehaviour
{
    public int wetness = 0; // 濡れた量
    public float wetnessPercentage = 0;
    [SerializeField] int wetnessSup = 200;

    void OnTriggerEnter2D(Collider2D other)
    {
        // 雨粒に当たったら濡れた量を1増やす
        if (other.CompareTag("raindrops"))
        {
            wetness += 1;
            Debug.Log("濡れた！現在の濡れた量: " + wetness);

            // 雨粒を消す（任意）
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            wetness = wetness - 1;
            Debug.Log("濡れた！現在の濡れた量: " + wetness);
        }
        wetnessPercentage = (float)wetness / (float)wetnessSup * 100f;
    }
}