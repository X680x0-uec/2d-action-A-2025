using Mono.Cecil.Cil;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    SpriteRenderer sr;
    Vector2 direction;
    public AudioSource audioSource;
    public AudioClip flapSound;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr.flipX)
        {
           
            direction = Vector2.right; //左へ進む
        }
        else
        {
            direction = Vector2.left;
        }
        
audioSource.clip = flapSound;
   
    audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        Move(direction); //左へ進む
        

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
