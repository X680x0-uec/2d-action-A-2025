using Unity.VisualScripting;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject spawnedBy;
    [SerializeField] GameObject Player;
    PlayerCheckerMaker playerCheckerMaker;
    float activateDistance;
    
    public void Initialize(GameObject caller)
    {
        spawnedBy = caller;
        playerCheckerMaker = spawnedBy.GetComponent<PlayerCheckerMaker>();
        activateDistance = playerCheckerMaker.activateDistance;

    }

    void Start()
    {
        Player = GameObject.FindWithTag("walker");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x - transform.position.x >= activateDistance)
        {
            spawnedBy.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
