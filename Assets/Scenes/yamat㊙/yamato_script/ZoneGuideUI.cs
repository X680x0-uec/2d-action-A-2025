
using UnityEngine;

public class ZoneGuideUI2D : MonoBehaviour
{
    public GameObject guideUI;
    private bool isDisplaying = false;
    private FieldObjectBase fob;

    private void Start()
    {
        fob = this.gameObject.GetComponent<FieldObjectBase>();
        if (guideUI != null)
            guideUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("walker") && guideUI != null && fob.isActioned == false)
        {
            guideUI.SetActive(true);
            isDisplaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("walker") && guideUI != null)
        {
            guideUI.SetActive(false);
            isDisplaying = false;
        }
    }

    private void Update()
    {
        if (isDisplaying && Input.GetMouseButtonDown(0)) // 左クリック
        {
            guideUI.SetActive(false);
            isDisplaying = false;
        }
    }
}
