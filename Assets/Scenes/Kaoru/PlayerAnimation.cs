using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 movement;
    private IEnumerator coroutine = null;
    public Collider2D targetCollider3;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        playerSpriteRenderer.flipX = false;

        animator.SetTrigger("DamagedByCar");

        yield return new WaitForSeconds(4.0f);

        playerSpriteRenderer.flipX = true;

        yield return new WaitForSeconds(2.0f);

        coroutine = null;
    }
}
