using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class rain_maker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject raindropPrefab;
    [SerializeField] public float range;
    [SerializeField] private float time;
    [SerializeField] Rigidbody2D player; // 速度参照用
    public float wind; //風の強さ
    public float moveSpeed; //雨の落ちる速さ
    public Vector2 moveDirection;
    public Vector2 spawnPosition;
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    void Update()
    {
        moveDirection = new Vector2(wind, -moveSpeed).normalized; // 動く向き獲得
    }
    // Update is called once per frame
    private IEnumerator SpawnLoop()
    {
        var i = 0;
        while (true)
        {
            spawnPosition = -moveDirection * 10f;
            spawnPosition += new Vector2(-moveDirection.y,moveDirection.x) * Random.Range(-range,range) + new Vector2(transform.position.x-10,transform.position.y);
            var rain = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity);
            spawnPosition = -moveDirection * 10f;
            spawnPosition += new Vector2(-moveDirection.y,moveDirection.x) * Random.Range(-range,range) + new Vector2(transform.position.x-10,transform.position.y);
            rain = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(time);
            i += 1;
            if (i == 10000) { break; }
        }
    }
}
