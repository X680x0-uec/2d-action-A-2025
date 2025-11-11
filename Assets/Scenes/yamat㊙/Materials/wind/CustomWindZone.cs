using DigitalRuby.RainMaker;
using UnityEngine;

public class CustomWindZone : MonoBehaviour
{
    public float minStrength = 1f;
    public float maxStrength = 5f;
    public float currentStrength;
    public Vector2 currentDirection;
    private bool playerInside = false;

    public WindUITextControllerTMP windTextController;

    public Vector2 WindForce => playerInside ? currentDirection * currentStrength : Vector2.zero;
    public bool IsPlayerInside => playerInside;
    public AudioSource audioSource;
    GameObject[] rains;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
             
             audioSource.Play();
            GameObject walker = GameObject.FindWithTag("walker");
            playerInside = true;
            currentStrength = Random.Range(minStrength, maxStrength);
            currentDirection = Random.value > 0.5f ? Vector2.right : Vector2.left;

            Debug.Log($"風エリアに入った: 方向={currentDirection}, 強さ={currentStrength}");
            windTextController.ShowWindInfo(currentDirection, currentStrength);

            // プレイヤーにこの風エリアを通知
            walker.GetComponent<PlayerController>()?.SetCurrentWindZone(this);
            // 雨にも通知
            rains = GameObject.FindGameObjectsWithTag("raindrops");
            foreach (GameObject rain in rains)
            {
                rain.GetComponent<RainScript2D>()?.SetCurrentWindZone(this);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInside = false;
            audioSource.Stop();
            windTextController.HideWindInfo();

            // プレイヤーから風エリアの参照を解除
            GameObject.FindWithTag("walker").GetComponent<PlayerController>()?.ClearWindZone(this);
            // 雨にも通知
            rains = GameObject.FindGameObjectsWithTag("raindrops");
            foreach (GameObject rain in rains)
            {
                rain.GetComponent<RainScript2D>()?.ClearWindZone(this);
            }
        }
    }
}
