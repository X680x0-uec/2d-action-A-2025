using UnityEngine;

public class umbrella_enemy_attackMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform transformActor;
    [SerializeField] GameObject actor;
    [SerializeField] Vector3 initialpos;
    [SerializeField] float distance; //吹っ飛ばされる距離
    [SerializeField] Vector3 currentpos;
    player_heal healmove;

    private Vector3 finalpos;
    void Start()
    {
        actor = GameObject.FindGameObjectWithTag("walker");
        transformActor = actor.GetComponent<Transform>();
        initialpos = transformActor.position; //Actorの衝突時位置
        finalpos = new Vector3(initialpos.x - distance, initialpos.y, initialpos.z);

        currentpos = initialpos;
        healmove = actor.GetComponent<player_heal>();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos.x -= 10f * Time.deltaTime;
        transformActor.position = currentpos;

        if (currentpos.x <= finalpos.x)
        {
            healmove.damaged = false;
            Destroy(this.gameObject);
        }
        if (transformActor.position.x <= -25f)
        {
            healmove.damaged = false;
            Destroy(this.gameObject);
        }
    }
}
