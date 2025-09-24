using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//ここ注意
public class first : MonoBehaviour
{
    public Text Firstscorehozon;
    public Text Secondscorehozon;
    public Text Thirdscorehozon;
    public Text Forthscorehozon;
    public Text Fifthscorehozon;
    public Button ResetButton;


    private int FirstscoreInt;
    private int SecondscoreInt;
    private int ThirdscoreInt;
    private int ForthscoreInt;
    private int FifthscoreInt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetButton.onClick.AddListener(ResetButtonClicked);
    }
    void Update()
    {
        FirstscoreInt = int.Parse(Firstscorehozon.text);
        PlayerPrefs.SetInt("1st", FirstscoreInt);

        SecondscoreInt = int.Parse(Secondscorehozon.text);
        PlayerPrefs.SetInt("2nd", SecondscoreInt);

        ThirdscoreInt = int.Parse(Thirdscorehozon.text);
        PlayerPrefs.SetInt("3rd", ThirdscoreInt);

        ForthscoreInt = int.Parse(Forthscorehozon.text);
        PlayerPrefs.SetInt("4th", ForthscoreInt);

        FifthscoreInt = int.Parse(Fifthscorehozon.text);
        PlayerPrefs.SetInt("5th", FifthscoreInt);

        
    }
    private void Awake()
    {
        FirstscoreInt = PlayerPrefs.GetInt("1st", FirstscoreInt);
        Firstscorehozon.text = FirstscoreInt.ToString();

        SecondscoreInt = PlayerPrefs.GetInt("2nd", SecondscoreInt);
        Secondscorehozon.text = SecondscoreInt.ToString();

        ThirdscoreInt = PlayerPrefs.GetInt("3rd", ThirdscoreInt);
        Thirdscorehozon.text = ThirdscoreInt.ToString();

        ForthscoreInt = PlayerPrefs.GetInt("4th", ForthscoreInt);
        Forthscorehozon.text = ForthscoreInt.ToString();

        FifthscoreInt = PlayerPrefs.GetInt("5th", FifthscoreInt);
        Fifthscorehozon.text = FifthscoreInt.ToString();
        
    }

    void ResetButtonClicked()
    {
        Firstscorehozon.text = "000000";
        Secondscorehozon.text = "000000";
        Thirdscorehozon.text = "000000";
        Forthscorehozon.text = "000000";
        Fifthscorehozon.text = "000000";
        
    }
}
