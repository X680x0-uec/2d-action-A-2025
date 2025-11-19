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
    public float kasaScorePercent;
    public int scoreMultiple;
    void Start()
    {
        //TimeinputField.onEndEdit.AddListener(EnterPressed);
        int totalscore = (int)GameUIManager.remaining * scoreMultiple;
        Debug.Log(totalscore); //900点+α 1000点で10%とか？
        float finaltime = GameUIManager.remaining;
        totalscore += (int)(totalscore * ((GameUIManager.score / 100) * (kasaScorePercent)))/100;
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
