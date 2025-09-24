using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tenterkeyinput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputField TimeinputField;
    public Text Timekariscore;

    void Start()
    {
        TimeinputField.onEndEdit.AddListener(EnterPressed);
    }
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
        
        Timekariscore.text = TimeinputField.text;
                //InputField コンポーネントを取得
        InputField form = GameObject.Find("TInputField (Legacy)").GetComponent<InputField>();
        form.text = "";

    }
}
