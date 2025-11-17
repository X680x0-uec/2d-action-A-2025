using UnityEngine;

public class DragRotate2D : MonoBehaviour
{
    public Transform pivot;
    public float tiltSpeed = 2f;

    private Vector2 initialMouseDir;
    private bool dragging = false;
    private Quaternion targetRotation;
    private bool windTiltApplied = false;

    private PlayerController player;
    public float dragSensitivity = 1f;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentMouseDir = (Vector2)(mouseWorldPos - pivot.position);

        if (Input.GetMouseButtonDown(1))
        {
            initialMouseDir = currentMouseDir.normalized;
            dragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            dragging = false;
        }

        if (dragging)
        {
            
if (dragging)
{
    float angleDelta = Vector2.SignedAngle(initialMouseDir, currentMouseDir.normalized);
    pivot.Rotate(0f, 0f, angleDelta * dragSensitivity); // 感度を反映
    initialMouseDir = currentMouseDir.normalized;
}

        }

        // 風による傾き処理
        if (player != null)
        {
            Vector2 windForce = player.GetWindForce();
            if (Mathf.Abs(windForce.x) > 0.01f && !windTiltApplied)
            {
                float tiltDirection = Mathf.Sign(windForce.x);
                float targetZ = -90f + (-tiltDirection * 90f);
                targetRotation = Quaternion.Euler(0f, 0f, targetZ);
                windTiltApplied = true;
            }

            if (windTiltApplied)
            {
                pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, Time.deltaTime * tiltSpeed);
            }

            if (player.GetWindForce() == Vector2.zero)
            {
                windTiltApplied = false;
            }
        }
    }
}
