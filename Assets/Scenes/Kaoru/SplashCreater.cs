using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SplashCreater : MonoBehaviour
{
    public GameObject SplashPrefab;
    private IEnumerator coroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Car") && coroutine == null)
        {
            coroutine = SplashCreate();

            StartCoroutine(coroutine);
        }
    }

    IEnumerator SplashCreate()
    {
        Instantiate(SplashPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);

        coroutine = null;
    }
}
