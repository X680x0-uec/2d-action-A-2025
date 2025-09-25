using System.Collections.Generic;
using DigitalRuby.RainMaker;
using TMPro;
using UnityEngine;

public class RainManagerScript : MonoBehaviour
{

    [Header("雨の設定(雨は指定したx座標を中心に展開 強さは0~1 幅は0より大きい値)")]

    public List<RainVector3> rainParamator = new List<RainVector3>();
    public GameObject Rain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int count = rainParamator.Count;
        for (int i = 0; i < count; i++)
        {
            var r = Instantiate(Rain, new Vector3(rainParamator[i].locate, 0, 0), Quaternion.identity);
            var s = r.GetComponent<RainScript2D>();
            s.RainIntensity = rainParamator[i].Strength;
            s.widthLimit = rainParamator[i].Width;
        }
    }
}

[System.Serializable]
public struct RainVector3
{
    public float locate;
    public float Strength;
    public float Width;
}
