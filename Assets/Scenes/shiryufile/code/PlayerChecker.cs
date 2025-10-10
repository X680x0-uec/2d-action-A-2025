using Unity.VisualScripting;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject spawnedBy;
    GameObject Player = GameObject.FindWithTag("Player");
    
    public void Initialize(GameObject caller)
    {
        spawnedBy = caller;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
