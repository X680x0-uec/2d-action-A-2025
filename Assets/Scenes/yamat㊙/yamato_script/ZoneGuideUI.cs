using UnityEngine;

public class ZoneGuideUI : MonoBehaviour
{
    public GameObject guideUI; // UI全体（Image + Text）

    private void Start()
    {
        if (guideUI != null)
            guideUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("walker") && guideUI != null)
        {
            guideUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("walker") && guideUI != null)
        {
            guideUI.SetActive(false);
        }
    }
}