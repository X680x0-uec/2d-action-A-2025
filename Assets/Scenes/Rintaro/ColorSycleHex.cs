using UnityEngine;

public class ColorSycleHex : MonoBehaviour
{
    public string colorCodeA;
    public string colorCodeB;
    public float cycleDuration = 1f;      // 周期（秒）

    private SpriteRenderer spriteRenderer;
    private Color colorA;
    private Color colorB;
    private FieldObjectBase kasawaza;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        kasawaza = GetComponent<FieldObjectBase>();
    }

    void Update()
    {
        if (!kasawaza.isActioned)
        {
            float t = (Mathf.Sin(Time.time * Mathf.PI * 2 / cycleDuration) + 1) / 2;
            spriteRenderer.color = Color.Lerp(colorA, colorB, t);

            // カラーコードをColorに変換
            if (!ColorUtility.TryParseHtmlString(colorCodeA, out colorA))
            {
                colorA = Color.white;
            }
            if (!ColorUtility.TryParseHtmlString(colorCodeB, out colorB))
            {
                colorB = Color.white;
            }
        } else
        {
            spriteRenderer.color = Color.white;
        }
    }
}