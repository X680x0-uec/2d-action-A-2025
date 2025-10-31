using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class birdMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    float SignedAngleIgnoreZ(Vector3 from3D, Vector3 to3D) //2ベクトルの回転角の計算．三次元だがz成分を捨てている．
    {
        Vector2 from2D = new Vector2(from3D.x, from3D.y);
        Vector2 to2D = new Vector2(to3D.x, to3D.y);
        return Vector2.SignedAngle(from2D, to2D);
    }
    Vector3 RotateInXY(Vector3 original, float signedAngleDegrees) //ある回転角だけベクトルを回転．三次元だがz成分を捨てている．
    {
        // Z成分を無視して2Dベクトルに変換
        Vector2 original2D = new Vector2(original.x, original.y);

        // 回転角をラジアンに変換
        float radians = signedAngleDegrees * Mathf.Deg2Rad;

        // 回転行列を適用
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        Vector2 rotated2D = new Vector2(
            original2D.x * cos - original2D.y * sin,
            original2D.x * sin + original2D.y * cos
        );

        // 元のZ成分を保持して3Dに戻す
        return new Vector3(rotated2D.x, rotated2D.y, original.z);
    }

    [SerializeField] Sprite walking;
    [SerializeField] Sprite flying;
    private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject Actor;
    private Transform transformActor;
    private Vector3 firstDirection;
    private bool attack = false;
    private float firstTime;
    private Vector3 firstActorPosition;
    player_heal healmove;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = walking;
        transformActor = Actor.GetComponent<Transform>();
        healmove = GameObject.FindWithTag("walker").GetComponent<player_heal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.sprite == walking && transform.position.x - transformActor.position.x <= 10f) //ある程度近づいたら飛び始める
        {
            spriteRenderer.sprite = flying;
            firstDirection = (transformActor.position - new Vector3(0,1,0)) - transform.position;
            firstDirection.z = 0f; //z軸方向に余計に動かないように調整
            firstDirection.Normalize(); //正規化
        }

        if (spriteRenderer.sprite == flying)
        {
            if (attack)
            {
                transform.position = firstActorPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), -2); //攻撃モーション．もっと良いのがあれば良いのだが…．
                transformActor.position = firstActorPosition; //動けなくする

                if (Time.time - firstTime >= 3f)  //一定時間たったら自分を削除
                {
                    Destroy(this.gameObject);
                    healmove.damaged = false;
                }
            }
            else
            {
                transform.position += firstDirection * 5f * Time.deltaTime;

                if (transform.position.x - transformActor.position.x >= 1f) //ちょっと追尾する
                {
                    float angle = SignedAngleIgnoreZ(firstDirection, (transformActor.position - new Vector3(0, 1, 0)) - transform.position);
                    firstDirection = RotateInXY(firstDirection, angle / 2);
                }
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "walker" && !attack)
        {
            attack = true;
            healmove.damaged = true;
            firstTime = Time.time;
            firstActorPosition = transformActor.position;
        }
        
    }
}
