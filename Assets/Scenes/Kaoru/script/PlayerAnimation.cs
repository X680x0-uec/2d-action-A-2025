
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 movement;
    private IEnumerator coroutine = null;
    public Collider2D targetCollider3;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    // プレイヤーのジャンプ状態を参照するための public 変数
    public PlayerJump playerJump;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Walk", movement.sqrMagnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Car") && coroutine == null)
        {
            if (other.IsTouching(targetCollider3))
            {
                coroutine = CarAccident();
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator CarAccident()
    {
        // ジャンプ中ならアニメーション処理をスキップ
        if (playerJump != null && playerJump.isJumping)
        {
            Debug.Log("ジャンプ中なのでアニメーション処理をスキップ");
            yield break;
        }

        playerSpriteRenderer.flipX = false;
        animator.SetTrigger("DamagedByCar");

        yield return new WaitForSeconds(4.0f);

        playerSpriteRenderer.flipX = true;

        yield return new WaitForSeconds(2.0f);

        coroutine = null;
    }
}
