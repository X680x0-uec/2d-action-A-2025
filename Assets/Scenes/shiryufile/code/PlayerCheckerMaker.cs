using UnityEngine;

public class PlayerCheckerMaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject checkerPrefab;
    bool isActive = false;
    public float activateDistance; //有効化するために近づく距離（主人公 - 本オブジェクト）
    void Awake()
    {
        if (!isActive)
        {

            GameObject prefabInstance = Instantiate(checkerPrefab, transform.position, transform.rotation);
            prefabInstance.GetComponent<PlayerChecker>().Initialize(this.gameObject);
            isActive = true;
            this.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
