using UnityEngine;

public class WetnessCounter : MonoBehaviour
{
    public Collider2D targetCollider; // 濡れ判定対象のコライダーを指定
    public int wetness = 0;
    public float wetnessPercentage = 0;
    [SerializeField] int wetnessSup = 200;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("raindrops"))
        {
            // 雨粒が targetCollider に当たった場合のみ濡れた量を加算
            if (other.IsTouching(targetCollider))
            {
                wetness = Mathf.Min(wetnessSup, wetness + 1);
                Debug.Log("濡れた！現在の濡れた量: " + wetness);
            }
            else
            {
                Debug.Log("濡れ判定対象外のコライダーに当たったので加算せず、雨粒を消去");
            }

            // 雨粒はどちらに当たっても消す
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        wetnessPercentage = (float)wetness / (float)wetnessSup * 100f;
    }
}
