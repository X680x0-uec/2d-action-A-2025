using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//ここ注意
public class Tfirst : MonoBehaviour
{
    public Text TFirstscorehozon;
    public Text TSecondscorehozon;
    public Text TThirdscorehozon;
    public Text TForthscorehozon;
    public Text TFifthscorehozon;
    public Button TResetButton;


    private int TFirstscoreInt;
    private int TSecondscoreInt;
    private int TThirdscoreInt;
    private int TForthscoreInt;
    private int TFifthscoreInt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TResetButton.onClick.AddListener(ResetButtonClicked);
    }
    void Update()
    {
        TFirstscoreInt = int.Parse(TFirstscorehozon.text);
        PlayerPrefs.SetInt("t1st", TFirstscoreInt);

        TSecondscoreInt = int.Parse(TSecondscorehozon.text);
        PlayerPrefs.SetInt("t2nd", TSecondscoreInt);

        TThirdscoreInt = int.Parse(TThirdscorehozon.text);
        PlayerPrefs.SetInt("t3rd", TThirdscoreInt);

        TForthscoreInt = int.Parse(TForthscorehozon.text);
        PlayerPrefs.SetInt("t4th", TForthscoreInt);

        TFifthscoreInt = int.Parse(TFifthscorehozon.text);
        PlayerPrefs.SetInt("t5th", TFifthscoreInt);

        
    }
    private void Awake()
    {
        TFirstscoreInt = PlayerPrefs.GetInt("t1st", TFirstscoreInt);
        TFirstscorehozon.text = TFirstscoreInt.ToString();

        TSecondscoreInt = PlayerPrefs.GetInt("t2nd", TSecondscoreInt);
        TSecondscorehozon.text = TSecondscoreInt.ToString();

        TThirdscoreInt = PlayerPrefs.GetInt("t3rd", TThirdscoreInt);
        TThirdscorehozon.text = TThirdscoreInt.ToString();

        TForthscoreInt = PlayerPrefs.GetInt("t4th", TForthscoreInt);
        TForthscorehozon.text = TForthscoreInt.ToString();

        TFifthscoreInt = PlayerPrefs.GetInt("t5th", TFifthscoreInt);
        TFifthscorehozon.text = TFifthscoreInt.ToString();
        
    }

    void ResetButtonClicked()
    {
        TFirstscorehozon.text = "000000";
        TSecondscorehozon.text = "000000";
        TThirdscorehozon.text = "000000";
        TForthscorehozon.text = "000000";
        TFifthscorehozon.text = "000000";
        
    }
}
