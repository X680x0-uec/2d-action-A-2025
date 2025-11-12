using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("ジャンプ設定")]
    public float jumpHeight = 3f;
    public float jumpDuration = 0.5f;

    [Header("影の設定")]
    public GameObject shadowObject;
    public float shadowOffsetY = 0.5f;

    [Header("ジャンプ中に無効化するスクリプト")]
    public MonoBehaviour[] scriptsToDisable;

    public bool isJumping { get; private set; } // 他スクリプトから参照可能

    private float jumpTimer = 0f;
    public float baseY;
    public float shadowY;

    private Collider2D playerCollider;
    private PlayerController PC;
    public AudioSource audioSource;
    public AudioClip jump;
    void Start()
    {
        if (shadowObject != null)
        {
            shadowObject.SetActive(false);
        }
        scriptsToDisable[1] = GetComponent<player_heal>();
        playerCollider = GetComponent<Collider2D>();
        PC = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!isJumping && Input.GetButtonDown("Jump") && !PC.damaged)
        {
            audioSource.clip = jump;
            audioSource.Play();
            StartJump();
            Debug.Log("jump");
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            baseY += PC.totalMovement.y * Time.deltaTime * ((shadowObject.GetComponent<Transform>().position.y <= -8.89 || shadowObject.GetComponent<Transform>().position.y >= -2.40)? 0f:1f);

            shadowY = shadowObject.transform.position.y;
            Vector3 pos = transform.position;
            pos.y = baseY + yOffset;
            transform.position = pos;

            if (shadowObject != null)
            {
                shadowObject.SetActive(true);
                shadowObject.transform.position = new Vector3(
                    transform.position.x,
                    baseY - shadowOffsetY,
                    transform.position.z
                );
            }

            if (normalizedTime >= 1f)
            {
                EndJump();
            }
        }
    }

    void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f;
        baseY = transform.position.y;

        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = false;
                Debug.Log($"スクリプト {script.GetType().Name} を無効化しました");
            }
        }

        if (playerCollider != null)
        {
            playerCollider.enabled = false; // 必要に応じてColliderも無効化
        }
    }

    void EndJump()
    {
        isJumping = false;

        Vector3 pos = transform.position;
        pos.y = baseY;
        transform.position = pos;

        if (shadowObject != null)
        {
            shadowObject.SetActive(false);
        }

        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                script.enabled = true;
                Debug.Log($"スクリプト {script.GetType().Name} を再有効化しました");
            }
        }

        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }
}
