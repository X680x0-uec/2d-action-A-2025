using UnityEngine;
using System.Collections;

public class SplashMoverDown : MonoBehaviour
{
    public float interval = 0.5f;
    public int damege1 = 10; // float → int に変更
    public int damege2 = 2;  // float → int に変更
    private float timer = 0f;
    private WetnessCounter wetnessCounter;
    private bool isPlayerInside = false;
    private bool hasAddedInitialWetness = false;
    public GameObject Actor;
    public PlayerJump playerJump;

    private void Start()
    {
        Actor = GameObject.Find("Actor");

        if (Actor != null)
        {
            playerJump = Actor.GetComponent<PlayerJump>();
        }

        StartCoroutine(Splash());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("walker"))
        {
            wetnessCounter = Actor.GetComponent<WetnessCounter>();
            if (!hasAddedInitialWetness && playerJump != null && !playerJump.isJumping)
            {
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + damege1, 200);
                hasAddedInitialWetness = true;
                timer = 0f;
                isPlayerInside = true;
                Debug.Log("水たまりに入った！濡れた量: " + wetnessCounter.wetness);
            }
        }

        if (other.CompareTag("umbrella"))
        {
            Destroy(this.gameObject);
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
        if (isPlayerInside && wetnessCounter != null && playerJump != null && !playerJump.isJumping)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                wetnessCounter.wetness = Mathf.Min(wetnessCounter.wetness + damege2, 200);
                timer = 0f;
                Debug.Log("濡れ続け中... 濡れた量: " + wetnessCounter.wetness);
            }
        }

        Move(Vector2.down);
    }

    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;

        var moveSpeed = 0.6f;

        pos += moveDirection * moveSpeed * Time.deltaTime;

        transform.position = pos;
    }

    IEnumerator Splash()
    {
        yield return new WaitForSeconds(2.5f);

        Destroy(this.gameObject);
    }
}
