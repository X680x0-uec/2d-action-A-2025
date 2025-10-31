using System;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class umbrella_enemyMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    //ここから主人公判定処理用変数
    [SerializeField] GameObject checkerPrefab;
    bool isActive = false;
    public float activateDistance; //有効化するために近づく距離（主人公 - 本オブジェクト）
    //ここまで*/
    [SerializeField] float fps;
    [SerializeField] float probability;
    [SerializeField] float rnd;
    [SerializeField] float rotationalSpeed;
    private Rigidbody2D rb;
    [SerializeField] GameObject actor;
    private Transform transformActor;
    [SerializeField] GameObject attacker;
    public player_heal healmove;
    void Start()
    {
        /*//ここから主人公判定処理用設定
        if (!isActive)
        {

            GameObject prefabInstance = Instantiate(checkerPrefab, transform.position, transform.rotation);
            prefabInstance.GetComponent<PlayerChecker>().Initialize(this.gameObject);
            isActive = true;
            this.gameObject.SetActive(false);

        }
        //ここまで*/
        rb = GetComponent<Rigidbody2D>();
        actor = GameObject.FindWithTag("walker");
        transformActor = actor.GetComponent<Transform>();
        rb.AddForce(new Vector2(-1f, 1.732f).normalized * 5f, ForceMode2D.Impulse);
        rotationalSpeed = UnityEngine.Random.Range(2f, 5f);
        healmove = actor.GetComponent<player_heal>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //現在のfpsを参考にして，今フレームの跳ね返る確率を決める．
        fps = 1f / Time.deltaTime;
        probability = 1f - (float)Math.Pow(2f, -1f / fps);
        rnd = UnityEngine.Random.Range(0f, 1f);

        //跳ね返る処理．一度鉛直速度を殺してから鉛直上向きに力積を与える．
        if ((rnd < probability & transform.position.y < -3.0f) || transform.position.y < -8.5f)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            rotationalSpeed = UnityEngine.Random.Range(2f, 5f);
        }
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationalSpeed); //傘をくるくる
        /*備忘録：transform.positionのx,y,zプロパティを直接変更することはできないので，このように書く．
        Vector3 pos = transformActor.position;
        pos.y = 0f;
        transformActor.position = pos;*/


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag == "walker");
        if (other.gameObject.tag == "walker")
        {
            healmove.damaged = true;
            Instantiate(attacker, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
