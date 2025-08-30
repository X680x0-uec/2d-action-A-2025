using System;
using Unity.VisualScripting;
using UnityEngine;

public class rain_move : MonoBehaviour
{

    GameObject rain_make;
    public rain_maker rain;
    float angle;
    Vector3 startPosition;
    void Start()
    {
        rain_make = GameObject.Find("raincreator");
        rain = rain_make.GetComponent<rain_maker>(); // 親のスクリプトゲット
        startPosition = transform.localPosition;

    }
    void FixedUpdate()
    {
        Move(rain.moveDirection);

        if ((startPosition - this.transform.localPosition).sqrMagnitude >= 625.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void Move(Vector3 moveDirection)
    {
        if (rain.wind >= 0)
        {
            angle = Vector3.Angle(rain.moveDirection, Vector3.down); //2つのベクトルのなす角の計算
        }
        else
        {
            angle = -Vector3.Angle(rain.moveDirection, Vector3.down); //なす角が負だった場合の処理
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        var pos = transform.position;
        pos += moveDirection * rain.moveSpeed * Time.deltaTime;
        transform.position = pos;
    }
}
