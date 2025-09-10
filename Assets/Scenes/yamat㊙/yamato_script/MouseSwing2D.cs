using UnityEngine;

public class DragRotate2D : MonoBehaviour
{
    public Transform pivot; // 回転の基点（プレイヤーや手元など）
    private Vector2 initialMouseDir;
    private bool dragging = false;

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
            // 変位ベクトルと初期ベクトルの角度差を計算
            float angleDelta = Vector2.SignedAngle(initialMouseDir, currentMouseDir.normalized);

            // 現在の角度に差分を加算
            pivot.rotation = Quaternion.Euler(0f, 0f, pivot.rotation.eulerAngles.z + angleDelta);

            // 初期ベクトルを更新して連続的に回転させる
            initialMouseDir = currentMouseDir.normalized;
        }
    }
}
