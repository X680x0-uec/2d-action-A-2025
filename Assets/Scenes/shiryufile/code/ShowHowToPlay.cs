using UnityEngine;
using UnityEngine.UI;

public class ShowHowToPlay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Image HowToPlayImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void actHowHowToPlay()
    {
        HowToPlayImage.gameObject.SetActive(true);
    }
}
