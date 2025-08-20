using UnityEngine;

public class Playermover : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float normalSpeed;
    void Start()
    {
        moveSpeed = normalSpeed; //初期化
    }
    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");

        var y = Input.GetAxisRaw("Vertical");

        var moveDirection = new Vector2(x, y).normalized;

        Move(moveDirection);
    }

    void Move(Vector3 moveDirection)
    {
        var pos = transform.position;

        pos += moveDirection * moveSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);

        transform.position = pos;
    }

}
