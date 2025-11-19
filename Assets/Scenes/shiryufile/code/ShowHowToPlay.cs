using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class ShowHowToPlay : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject howToPlayPanel;
    public Image howToPlayImage;
    [SerializeField] private Sprite[] howToPlayPagesForKeyboard;
    [SerializeField] private Sprite[] howToPlayPagesForGamepad;
    [SerializeField] private Sprite[] howToPlayPages;

    private int currentPage = 0;
    private bool isShowingHowToPlay = false;
    private Gamepad gamepad;
    void Update()
    {
        if (isShowingHowToPlay && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")))
        {
            AdvancePage();
        }
    }


    public void actHowToPlay()
    {
        titlePanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        currentPage = 0;
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            howToPlayPages = howToPlayPagesForKeyboard;
        }
        else
        {
            howToPlayPages = howToPlayPagesForGamepad;
        }
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
            StartCoroutine(WaitUntilReady()); //コルーチンの使い方あってるのか……．
        }
    }
        IEnumerator WaitUntilReady()
    {
        Debug.Log("待機開始...");
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("joystick button 0")); // 条件がtrueになるまで待機
        Debug.Log("条件が満たされました！");
        // ここから先の処理が実行される
        howToPlayPanel.SetActive(false);
        titlePanel.SetActive(true);
        isShowingHowToPlay = false;
    }



}
