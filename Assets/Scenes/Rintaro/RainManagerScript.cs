using System.Collections.Generic;
using DigitalRuby.RainMaker;
using TMPro;
using UnityEngine;

public class RainManagerScript : MonoBehaviour
{

    [Header("雨の追加＆編集")]

    public List<RainVector3> rainParamator = new List<RainVector3>();

    [System.Serializable]
    public struct RainVector3
    {
        public float locate;
        //雨の場所(左端基準)
        public float Strength;
        //雨の強さ(0~1 0.5から霧が出てくる)
        public float Width;
        //雨の広さ(0より大きい値 x座標換算)
    }

    public GameObject Rain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int count = rainParamator.Count;
        for (int i = 0; i < count; i++)
        {
            var r = Instantiate(Rain, new Vector3(rainParamator[i].locate+(rainParamator[i].Width/2), 0, 0), Quaternion.identity);
            var s = r.GetComponent<RainScript2D>();
            s.RainIntensity = rainParamator[i].Strength;
            s.widthLimit = rainParamator[i].Width;
        }
    }
}


