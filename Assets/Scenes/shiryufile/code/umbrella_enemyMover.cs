using System;
using Unity.VisualScripting;
using UnityEngine;

public class umbrella_enemyMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float fps;
    [SerializeField] float probability;
    [SerializeField] float rnd;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-1f, 1.732f).normalized * 5f, ForceMode2D.Impulse);

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
        }
    }
}
