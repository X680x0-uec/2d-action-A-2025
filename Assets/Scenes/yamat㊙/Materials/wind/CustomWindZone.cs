using UnityEngine;

public class CustomWindZone : MonoBehaviour
{
    public float minStrength = 1f;
    public float maxStrength = 5f;
    private float currentStrength;
    private Vector2 currentDirection;
    private bool playerInside = false;

    public WindUITextControllerTMP windTextController;

    public Vector2 WindForce => playerInside ? currentDirection * currentStrength : Vector2.zero;
    public bool IsPlayerInside => playerInside;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("walker"))
        {
            playerInside = true;
            currentStrength = Random.Range(minStrength, maxStrength);
            currentDirection = Random.value > 0.5f ? Vector2.right : Vector2.left;

            Debug.Log($"風エリアに入った: 方向={currentDirection}, 強さ={currentStrength}");
            windTextController.ShowWindInfo(currentDirection, currentStrength);

            // プレイヤーにこの風エリアを通知
            other.GetComponent<PlayerController>()?.SetCurrentWindZone(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("walker"))
        {
            playerInside = false;
            windTextController.HideWindInfo();

            // プレイヤーから風エリアの参照を解除
            other.GetComponent<PlayerController>()?.ClearWindZone(this);
        }
    }
}
