using UnityEngine;
using UnityEngine.UI;

public class ShowHowToPlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image HowToPlayImage;
    public void actHowHowToPlay()
    {
        HowToPlayImage.gameObject.SetActive(true);
    }
}
