using UnityEngine;
using UnityEngine.UI;

public class ShowHowToPlay : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject howToPlayPanel;
    public Image howToPlayImage;
    public Sprite[] howToPlayPages;

    private int currentPage = 0;
    private bool isShowingHowToPlay = false;

    void Update()
    {
        if (isShowingHowToPlay && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            AdvancePage();
        }
    }


    public void actHowToPlay()
    {
        titlePanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        currentPage = 0;
        howToPlayImage.sprite = howToPlayPages[currentPage];
        isShowingHowToPlay = true;

    }

        private void AdvancePage()
    {
        currentPage++;
        if (currentPage < howToPlayPages.Length)
        {
            howToPlayImage.sprite = howToPlayPages[currentPage];
        }
        else
        {
            howToPlayPanel.SetActive(false);
            titlePanel.SetActive(true);
            isShowingHowToPlay = false;
        }
    }

}
