using System.Collections;
using System.Security.Cryptography;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float normalSpeed; // 基準の速さ(傘技成功/失敗時に速度を操るために参照する値)
    private Rigidbody2D rb;
    private Vector2 movement;
<<<<<<< HEAD
    [SerializeField] MultiStageGauge2 wetGage;
=======
    GameObject CAMERA;
    int flag = 0;
    private IEnumerator coroutine = null;
    public Collider2D targetCollider2;
>>>>>>> 9fac05c4150e5f1a2ddc73e6ec1caa6329b77d4a

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = normalSpeed; // 初期化
        CAMERA = GameObject.Find("Main Camera");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        switch (flag)
        {
            case 1:
                goto case 2;
            case 3:
                CAMERA.transform.Translate(30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x >= transform.position.x + 1.0f)
                    flag++;
                break;
            case 2:
                CAMERA.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x <= transform.position.x + -1.0f)
                    flag++;
                break;
            case 4:
                CAMERA.transform.Translate(-30 * Time.deltaTime, 0, 0);
                if (CAMERA.transform.position.x <= transform.position.x + 0)
                    flag = 0;
                break;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed * (10-wetGage.levelOfWetness)/10;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Car") && coroutine == null)
        {
            if (other.IsTouching(targetCollider2))
            {
                coroutine = Wait();

                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator Wait()
    {
        var v1 = new Vector2(-70, 70);

        var posOrigin = transform.position;

        var posCameraOrigin = CAMERA.transform.position;

        yield return null;

        flag = 1;

        moveSpeed = 0;

        yield return null;

        Accident(v1);

        yield return new WaitForSeconds(4.0f);

        moveSpeed = normalSpeed;

        transform.position = posOrigin;

        CAMERA.transform.position = posCameraOrigin;

        yield return new WaitForSeconds(2.0f);

        coroutine = null;
    }

    void Accident(Vector3 moveDirection)
    {
        var posNew = transform.position;

        posNew += moveDirection * 6f * Time.deltaTime;

        transform.position = posNew;
    }
}
