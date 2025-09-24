using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//ここ注意


public class narabekae : MonoBehaviour
{
    public Text Clearscore;
    public Text Firstscore;
    public Text Secondscore;
    public Text Thirdscore;
    public Text Forthscore;
    public Text Fifthscore;

    private int ClearscoreInt;
    private int FirstscoreInt;
    private int SecondscoreInt;
    private int ThirdscoreInt;
    private int ForthscoreInt;
    private int FifthscoreInt;

    private int previousClearScore = -1; // 前回のスコアを保持

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    void Update()
    {
        int currentscore = int.Parse(Clearscore.text);
        if (currentscore != previousClearScore)
        {
            previousClearScore = currentscore;
            Scorechanged();
        }
    }

    void Scorechanged()
    {

        ClearscoreInt = int.Parse(Clearscore.text);
        FirstscoreInt = int.Parse(Firstscore.text);
        SecondscoreInt = int.Parse(Secondscore.text);
        ThirdscoreInt = int.Parse(Thirdscore.text);
        ForthscoreInt = int.Parse(Forthscore.text);
        FifthscoreInt = int.Parse(Fifthscore.text);




        // Update is called once per frame
        if (ClearscoreInt >= FirstscoreInt)
        {
            Fifthscore.text = Forthscore.text;
            Forthscore.text = Thirdscore.text;
            Thirdscore.text = Secondscore.text;
            Secondscore.text = Firstscore.text;
            Firstscore.text = Clearscore.text;
        }
        else if (ClearscoreInt >= SecondscoreInt)
        {
            Fifthscore.text = Forthscore.text;
            Forthscore.text = Thirdscore.text;
            Thirdscore.text = Secondscore.text;
            Secondscore.text = Clearscore.text;
        }
        else if (ClearscoreInt >= ThirdscoreInt)
        {
            Fifthscore.text = Forthscore.text;
            Forthscore.text = Thirdscore.text;
            Thirdscore.text = Clearscore.text;
        }
        else if (ClearscoreInt >= ForthscoreInt)
        {
            Fifthscore.text = Forthscore.text;
            Forthscore.text = Clearscore.text;
        }
        else if (ClearscoreInt >= FifthscoreInt)
        {
            Fifthscore.text = Clearscore.text;
        }
    }
}
