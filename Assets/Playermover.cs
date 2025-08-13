using UnityEngine;

public class Playermover : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
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

        moveSpeed = 4f;

        pos += moveDirection * moveSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);

        transform.position = pos;
    }

}
