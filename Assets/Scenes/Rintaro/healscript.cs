using UnityEngine;

public class healscript : MonoBehaviour
{
    [SerializeField] int heal;
    public Collider2D Actor;
    private WetnessCounter wetnessCounter;
    private PlayerJump playerJump;
    private void Start()
    {
        Actor = GameObject.FindWithTag("walker").GetComponent<Collider2D>();
        // walkerというタグが付いたオブジェクトを探す
        // プレハブ化しても勝手に検索してくれるので楽
        if (Actor != null)
        {
            playerJump = Actor.GetComponent<PlayerJump>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == Actor)
        {
            wetnessCounter = Actor.GetComponent<WetnessCounter>();
            if (playerJump != null && !playerJump.isJumping)
            {
                wetnessCounter.wetness = Mathf.Max(wetnessCounter.wetness - heal, 0);
                Debug.Log("ちょい回復～濡れた量: " + wetnessCounter.wetness);
                Destroy(this.gameObject);
            }
        }
    }

}
