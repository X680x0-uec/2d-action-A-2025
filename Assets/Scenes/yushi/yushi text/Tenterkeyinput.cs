using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tenterkeyinput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // public InputField TimeinputField;
    public Text Timekariscore;
    public Text kariscore;
    string time;
    void Start()
    {
        //TimeinputField.onEndEdit.AddListener(EnterPressed);
        int totalscore = GameUIManager.score;
        float finaltime = 600 - GameUIManager.remaining;
        int minute = (int)(finaltime/60);
        int second = (int)(finaltime%60);
        time = $"{minute:D2}" +":"+ $"{second:D2}";
        Timekariscore.text = time;
        kariscore.text = totalscore.ToString();
    }
    /*
    void OnDestroy()
    {
        TimeinputField.onEndEdit.RemoveListener(EnterPressed);
    }

    private void EnterPressed(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // ここで任意の関数を呼び出します
            YourFunction();
        }
    }

    private void YourFunction()
    {
        // Enterキーが押された時に実行するコード
        Timekariscore.text = time;
                //InputField コンポーネントを取得
        InputField form = GameObject.Find("TInputField (Legacy)").GetComponent<InputField>();
        form.text = "";
    }
    */
}
