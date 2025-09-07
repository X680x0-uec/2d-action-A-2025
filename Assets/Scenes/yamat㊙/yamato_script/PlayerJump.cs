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
    private float baseY;

    private Collider2D playerCollider;

    void Start()
    {
        if (shadowObject != null)
        {
            shadowObject.SetActive(false);
        }

        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isJumping && Input.GetButtonDown("Jump"))
        {
            StartJump();
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;
            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;

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
