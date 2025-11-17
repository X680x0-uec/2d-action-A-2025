using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Multiplayer.Center.Common;
using JetBrains.Annotations;//ここ注意


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
        int Tcurrentscore = (int)GameUIManager.remaining;
        if (Tcurrentscore != TpreviousClearScore)
        {
            TpreviousClearScore = Tcurrentscore;
            Scorechanged();
        }
    }

    int TimeStringToInt(string time)
    {
        if (time == "00:00")
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

    void Scorechanged()
    {

        TClearscoreInt = TimeStringToInt(TClearscore.text);
        TFirstscoreInt = TimeStringToInt(TFirstscore.text);
        TSecondscoreInt = TimeStringToInt(TSecondscore.text);
        TThirdscoreInt = TimeStringToInt(TThirdscore.text);
        TForthscoreInt = TimeStringToInt(TForthscore.text);
        TFifthscoreInt = TimeStringToInt(TFifthscore.text);




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
