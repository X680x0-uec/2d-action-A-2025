using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float normalSpeed; // 基準の速さ(傘技成功/失敗時に速度を操るために参照する値)
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = normalSpeed; // 初期化
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
