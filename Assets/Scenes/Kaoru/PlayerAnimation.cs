
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 movement;
    private IEnumerator coroutine = null;
    public Collider2D targetCollider3;
    public AudioClip ClashSound;
  public AudioSource audioSource;

    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    // プレイヤーのジャンプ状態を参照するための public 変数
    public PlayerJump playerJump;
    public PlayerController PC;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerJump = this.GetComponentInParent<PlayerJump>();
        PC = GameObject.FindWithTag("walker").GetComponent<PlayerController>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Walk", movement.sqrMagnitude);
        Debug.Log(playerJump.isJumping);
    }

    /*呼び出しをプレイヤー側から行うように変更したのでコメント化
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
    */

    public IEnumerator CarAccident()
    {
        // ジャンプ中ならアニメーション処理をスキップ
        /*
        if (playerJump != null && playerJump.isJumping)
        {
            Debug.Log("ジャンプ中なのでアニメーション処理をスキップ");
            yield break;
        }
        */
        animator.SetTrigger("DamagedByCar");
        audioSource.clip = ClashSound;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        PC.Accident(new Vector3(0,0,0));
        yield return new WaitForSeconds(5.5f);
        coroutine = null;
    }
}
