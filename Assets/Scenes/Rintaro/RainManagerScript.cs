using System.Collections.Generic;
using DigitalRuby.RainMaker;
using TMPro;
using UnityEngine;

public class RainManagerScript : MonoBehaviour
{

    [Header("雨の置く場所(x座標)")]

    public List<float> rainPlace;
    [Header("雨の設定(強さは0~1 幅は0以上の値)")]

    public List<RainVector2> rainParamator = new List<RainVector2>();
    public GameObject Rain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int count = rainPlace.Count;
        for (int i = 0; i < count; i++)
        {
            var r = Instantiate(Rain, new Vector3(rainPlace[i], 0, 0), Quaternion.identity);
            var s = r.GetComponent<RainScript2D>();
            s.RainIntensity = rainParamator[i].Strength;
            s.widthLimit = rainParamator[i].Width;
        }
    }
}

[System.Serializable]
public struct RainVector2
{
    public float Strength;
    public float Width;
}
