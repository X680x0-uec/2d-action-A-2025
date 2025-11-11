using UnityEngine;

public class Puddle : MonoBehaviour
{
    public float interval = 0.5f;
    public int damege1 = 10; // float → int に変更
    public int damege2 = 2;  // float → int に変更
    public AudioSource audioSource;
public AudioClip ameSound;


    private float timer = 0f;
    private WetnessCounter wetnessCounter;
    private bool isPlayerInside = false;
    private bool hasAddedInitialWetness = false;

    [SerializeField] Collider2D Actor;

    private PlayerJump playerJump;

    private void Start()
    {
        if (Actor != null)
        {
            playerJump = Actor.GetComponent<PlayerJump>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Actor)
        {
            wetnessCounter = Actor.GetComponent<WetnessCounter>();
            if (!hasAddedInitialWetness && playerJump != null && !playerJump.isJumping)
            {
                audioSource.PlayOneShot(ameSound); // 音を再生
                audioSource.loop = true;
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + damege1, wetnessCounter.wetnessSup);
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
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (isPlayerInside && wetnessCounter != null && playerJump != null && !playerJump.isJumping)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + damege2, wetnessCounter.wetnessSup);
                timer = 0f;
                Debug.Log("濡れ続け中... 濡れた量: " + wetnessCounter.wetness);
            }
        }
        
    }
}
