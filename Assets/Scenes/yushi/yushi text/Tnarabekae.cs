using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;//ここ注意


public class Tnarabekae : MonoBehaviour
{
    public Text TClearscore;
    public Text TFirstscore;
    public Text TSecondscore;
    public Text TThirdscore;
    public Text TForthscore;
    public Text TFifthscore;

    private int TClearscoreInt;
    private int TFirstscoreInt;
    private int TSecondscoreInt;
    private int TThirdscoreInt;
    private int TForthscoreInt;
    private int TFifthscoreInt;

    private int TpreviousClearScore = -1; // 前回のスコアを保持

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    void Update()
    {
        int Tcurrentscore = int.Parse(TClearscore.text);
        if (Tcurrentscore != TpreviousClearScore)
        {
            TpreviousClearScore = Tcurrentscore;
            Scorechanged();
        }
    }

    void Scorechanged()
    {

        TClearscoreInt = int.Parse(TClearscore.text);
        TFirstscoreInt = int.Parse(TFirstscore.text);
        TSecondscoreInt = int.Parse(TSecondscore.text);
        TThirdscoreInt = int.Parse(TThirdscore.text);
        TForthscoreInt = int.Parse(TForthscore.text);
        TFifthscoreInt = int.Parse(TFifthscore.text);




        // Update is called once per frame
        if (TClearscoreInt >= TFirstscoreInt)
        {
            TFifthscore.text = TForthscore.text;
            TForthscore.text = TThirdscore.text;
            TThirdscore.text = TSecondscore.text;
            TSecondscore.text = TFirstscore.text;
            TFirstscore.text = TClearscore.text;
        }
        else if (TClearscoreInt >= TSecondscoreInt)
        {
            TFifthscore.text = TForthscore.text;
            TForthscore.text = TThirdscore.text;
            TThirdscore.text = TSecondscore.text;
            TSecondscore.text = TClearscore.text;
        }
        else if (TClearscoreInt >= TThirdscoreInt)
        {
            TFifthscore.text = TForthscore.text;
            TForthscore.text = TThirdscore.text;
            TThirdscore.text = TClearscore.text;
        }
        else if (TClearscoreInt >= TForthscoreInt)
        {
            TFifthscore.text = TForthscore.text;
            TForthscore.text = TClearscore.text;
        }
        else if (TClearscoreInt >= TFifthscoreInt)
        {
            TFifthscore.text = TClearscore.text;
        }
    }
}
