using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using DigitalRuby.RainMaker;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
public float moveSpeed;
    public float normalSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;

    private CustomWindZone currentWindZone;

    [SerializeField] MultiStageGauge2 wetGage;
    [SerializeField] GameObject CAMERA;
    public int flag = 0;
    private IEnumerator coroutine = null;
    public Collider2D targetCollider2;
    [SerializeField] float shakeSpeed;
    [SerializeField] GameObject grip;



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
    }

   

void FixedUpdate()
{
    Vector2 windForce = currentWindZone != null ? currentWindZone.WindForce : Vector2.zero;
    Vector2 totalMovement = movement.normalized * moveSpeed * (10 - wetGage.levelOfWetness) / 10 + windForce;
    rb.linearVelocity = totalMovement;
}


public void SetCurrentWindZone(CustomWindZone zone)
    {
        currentWindZone = zone;
    }

    public void ClearWindZone(CustomWindZone zone)
    {
        if (currentWindZone == zone)
        {
            currentWindZone = null;
        }
    }

    public Vector2 GetWindForce()
    {
        return currentWindZone != null ? currentWindZone.WindForce : Vector2.zero;
    }

  
void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Car") && coroutine == null)
    {
        if (other.IsTouching(targetCollider2))
        {
            // ジャンプ中なら処理をスキップ
            PlayerJump playerJump = GetComponent<PlayerJump>();
            if (playerJump != null && playerJump.isJumping)
            {
                Debug.Log("ジャンプ中なので車との衝突処理をスキップ");
                return;
            }

            coroutine = Wait();
            StartCoroutine(coroutine);
        }
    }
}

    IEnumerator shake()
    {
        float storeX = CAMERA.transform.position.x;
        switch (flag)
        {
            case 1:
                flag++;
                goto case 2;
            case 3:
                while (CAMERA.transform.position.x <= storeX + 0.2f)
                {
                    CAMERA.transform.Translate(shakeSpeed, 0, 0);
                    yield return null;
                }
                flag++;
                goto case 4;
            case 2:
                while (CAMERA.transform.position.x >= storeX - 0.2f)
                {
                    CAMERA.transform.Translate(-shakeSpeed, 0, 0);
                    yield return null;
                }
                flag++;
                goto case 3;
            case 4:
                while (CAMERA.transform.position.x >= storeX)
                {
                    CAMERA.transform.Translate(-shakeSpeed, 0, 0);
                    yield return null;
                }
                flag = 0;
                break;
        }
        CAMERA.transform.position = new Vector3(storeX,CAMERA.transform.position.y,CAMERA.transform.position.z);
        yield return null;
    }
    IEnumerator Wait()
    {
        var v1 = new Vector2(0, 1);
        grip.SetActive(false);

        var posOrigin = transform.position;

        yield return null;

        flag = 1;
        StartCoroutine(shake());
        moveSpeed = 0;

        yield return null;

        Accident(v1);

        yield return new WaitForSeconds(4.0f);
        grip.SetActive(true);

        moveSpeed = normalSpeed;

        transform.position = posOrigin;

        yield return new WaitForSeconds(2.0f);

        coroutine = null;
    }

    void Accident(Vector3 moveDirection)
    {
        var posNew = transform.position;
        posNew = new Vector3(transform.position.x,-1,-1);
        transform.position = posNew;
    }
}
