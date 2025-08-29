using Mono.Cecil.Cil;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Vector2.left); //左へ進む

        if (transform.position.x <= -20.0f)
        {
            Destroy(this.gameObject);
        }
        //画面外の位置まで進んだら消える        
    }


    private void Move(Vector3 moveDirection)
    {
        var pos = transform.position;

        pos += moveDirection * moveSpeed * Time.deltaTime;

        transform.position = pos;
    }
}
