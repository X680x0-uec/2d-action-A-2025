using UnityEngine;
using TMPro;

public class WindUITextControllerTMP : MonoBehaviour
{
    public GameObject windTextObject; // TextMeshProのGameObject
    public TextMeshProUGUI windText;  // TextMeshProUGUIコンポーネント

    public void ShowWindInfo(Vector2 direction, float strength)
    {
        windTextObject.SetActive(true);

        string dirText = direction == Vector2.right ? "右向き" : "左向き";
        windText.text = $"風の向き: {dirText}\n風の強さ: {strength:F1}";
    }

    public void HideWindInfo()
    {
        windTextObject.SetActive(false);
    }
}
