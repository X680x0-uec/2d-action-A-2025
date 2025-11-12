using Unity.VisualScripting;
using UnityEngine;

public class guardGameCanvasSettings : MonoBehaviour
{
    public Transform Playerxyz;
    private RectTransform canvasxyz;
    private Camera mainCamera;
    public float slider;
    public Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Playerxyz = GameObject.FindWithTag("walker").GetComponent<Transform>();
        canvasxyz = GetComponent<RectTransform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        
        // 初期オフセットを記録
        offset = canvasxyz.position - Playerxyz.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        // 親に追従
        canvasxyz.position = new Vector2(Playerxyz.position.x , Playerxyz.position.y) + offset;

        // ビューポート座標に変換（0～1）
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(canvasxyz.position);

        // Clampで画面内に収める（少し余白を残す）
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);

        // ワールド座標に戻す
        canvasxyz.position = mainCamera.ViewportToWorldPoint(viewportPos);

    }
}
