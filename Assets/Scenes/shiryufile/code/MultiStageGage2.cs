using UnityEngine;
using UnityEngine.UI;

public class MultiStageGauge2 : MonoBehaviour
{
    public Image[] stageImages; // Stage1〜Stage5を登録。unity画面上でやる
    [Range(0f, 100f)]
    //public float statusPercentage = 0f; // 0〜100のステータス
    [SerializeField] GameObject target; //targetに入るのは、WetnessCounter.csのついているオブジェクト
    public WetnessCounter Wetnesscounter; //別にWetnessCounterというスクリプト(コンポーネント？)があるので、それを引用するためにまず変数宣言
    public float statusPercentage;
    public int levelOfWetness; //濡れレベル

    void Start()
    {
        Wetnesscounter = target.GetComponent<WetnessCounter>(); //実際に代入
    }

    void Update()
    {
       
        statusPercentage = Wetnesscounter.wetnessPercentage; //WetnessCounterというスクリプト内の変数wetnessPercentageを代入
        float perStage = 100f / stageImages.Length;  //ここから下は、実際のuiを動かす処理

        for (int i = 0; i < stageImages.Length; i++)
        {
            float lowerBound = i * perStage;
            float upperBound = (i + 1) * perStage;

            if (statusPercentage >= upperBound)
            {
                stageImages[i].fillAmount = 1f;
            }
            else if (statusPercentage <= lowerBound)
            {
                stageImages[i].fillAmount = 0f;
            }
            else
            {
                float localPercent = (statusPercentage - lowerBound) / perStage;
                stageImages[i].fillAmount = localPercent;
                levelOfWetness = i;
            }
        }
    }
}