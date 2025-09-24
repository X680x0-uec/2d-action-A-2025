using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class enterkeyinput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputField inputField;
    public Text kariscore;

    void Start()
    {
        inputField.onEndEdit.AddListener(EnterPressed);
    }
    void OnDestroy()
    {
        inputField.onEndEdit.RemoveListener(EnterPressed);
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
        
        kariscore.text = inputField.text;
                //InputField コンポーネントを取得
        InputField form = GameObject.Find("SInputField (Legacy)").GetComponent<InputField>();
        form.text = "";

    }
}
