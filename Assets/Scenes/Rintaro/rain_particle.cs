using UnityEngine;

public class rain_particle : MonoBehaviour
{
    [SerializeField] float wind;
    public Vector2 moveDirection;
    public float moveSpeed;
    float angle;

    void Update()
    {
        moveDirection = new Vector2(wind, -moveSpeed).normalized;
        if (wind >= 0)
        {
            angle = Vector3.Angle(moveDirection, Vector3.down); //2つのベクトルのなす角の計算
        }
        else
        {
            angle = -Vector3.Angle(moveDirection, Vector3.down); //なす角が負だった場合の処理
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
