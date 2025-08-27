using UnityEngine;

public class Puddle : MonoBehaviour
{
    public float interval = 0.5f;
    public float damege1 = 10;
    public float damege2 = 2;
    private float timer = 0f;
    private WetnessCounter wetnessCounter;
    private bool isPlayerInside = false;
    private bool hasAddedInitialWetness = false;
    [SerializeField] Collider2D Actor;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Actor)
        {
            wetnessCounter = Actor.GetComponent<WetnessCounter>();
            if (!hasAddedInitialWetness)
            {
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + 10, 200);
                hasAddedInitialWetness = true;
                timer = 0f;
                isPlayerInside = true;
                Debug.Log("水たまりに入った！濡れた量: " + wetnessCounter.wetness);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<WetnessCounter>() == wetnessCounter)
        {
            isPlayerInside = false;
            hasAddedInitialWetness = false;
            wetnessCounter = null;
        }
    }

    private void Update()
    {
        Debug.Log(isPlayerInside);
        Debug.Log(wetnessCounter);
        if (isPlayerInside && wetnessCounter != null)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + 2, 200);
                timer = 0f;
                Debug.Log("濡れ続け中... 濡れた量: " + wetnessCounter.wetness);
            }
        }
    }
}
