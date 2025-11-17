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
    int TimeStringToInt(string time)
    {
        if (time == "0")
        {
            return 0;
        }
        string[] parts = time.Split(':');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        return minutes * 60 + seconds;
    }
    string SecondsToTimeString(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }
    void Update()
    {
        TFirstscoreInt = TimeStringToInt(TFirstscorehozon.text);
        PlayerPrefs.SetInt("t1st", TFirstscoreInt);

        TSecondscoreInt = TimeStringToInt(TSecondscorehozon.text);
        PlayerPrefs.SetInt("t2nd", TSecondscoreInt);

        TThirdscoreInt = TimeStringToInt(TThirdscorehozon.text);
        PlayerPrefs.SetInt("t3rd", TThirdscoreInt);

        TForthscoreInt = TimeStringToInt(TForthscorehozon.text);
        PlayerPrefs.SetInt("t4th", TForthscoreInt);

        TFifthscoreInt = TimeStringToInt(TFifthscorehozon.text);
        PlayerPrefs.SetInt("t5th", TFifthscoreInt);

        
    }
    private void Awake()
    {
        TFirstscoreInt = PlayerPrefs.GetInt("t1st", TFirstscoreInt);
        TFirstscorehozon.text = SecondsToTimeString(TFirstscoreInt);

        TSecondscoreInt = PlayerPrefs.GetInt("t2nd", TSecondscoreInt);
        TSecondscorehozon.text = SecondsToTimeString(TSecondscoreInt);

        TThirdscoreInt = PlayerPrefs.GetInt("t3rd", TThirdscoreInt);
        TThirdscorehozon.text = SecondsToTimeString(TThirdscoreInt);

        TForthscoreInt = PlayerPrefs.GetInt("t4th", TForthscoreInt);
        TForthscorehozon.text = SecondsToTimeString(TForthscoreInt);

        TFifthscoreInt = PlayerPrefs.GetInt("t5th", TFifthscoreInt);
        TFifthscorehozon.text = SecondsToTimeString(TFifthscoreInt);
        
    }

    void ResetButtonClicked()
    {
        TFirstscorehozon.text = "00:00";
        TSecondscorehozon.text = "00:00";
        TThirdscorehozon.text = "00:00";
        TForthscorehozon.text = "00:00";
        TFifthscorehozon.text = "00:00";
        
    }
}
