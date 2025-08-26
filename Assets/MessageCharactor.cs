using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MessageCharactor : FieldObjectBase
{
    [SerializeField] private string messages;
    [SerializeField] private int pushGoal;
    [SerializeField] GameUIManager GUM;
    [SerializeField] int scoreAmount;
    //  親クラスから呼ばれるコールバックメソッド（接触時に実行）
    //　時間制限→オートスクロールで勝手に接触が切れる

    protected override IEnumerator OnAction()
    {
        isActioned = true;
        int i = 0;
        for (i = 0; i < pushGoal; ++i)
        {
            yield return null;

            showMessage(messages + " " + (pushGoal - i) + "!");

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || !isContacted);

            if (!isContacted)
            {
                break;
            }
        }

        PlayerController PlayerController;
        GameObject obj = GameObject.Find("Actor");
        PlayerController = obj.GetComponent<PlayerController>();
        WetnessCounter wet = obj.GetComponent<WetnessCounter>(); // 濡れゲージ取得

        if (i == pushGoal)
        {
            showMessage("Success! 服が乾いて身軽になった気がする");
            PlayerController.moveSpeed = 2 * PlayerController.normalSpeed;
            wet.wetness = Mathf.Max(wet.wetness - 20, 0);
            GUM.AddScore(scoreAmount);
            yield return new WaitForSeconds(5f);
            PlayerController.moveSpeed = PlayerController.normalSpeed;
            yield break;
        }
        else
        {
            showMessage("Failed... びちょ濡れで足取りが重くなった");
            PlayerController.moveSpeed = 0.5f * PlayerController.normalSpeed;
            wet.wetness = Mathf.Max(wet.wetness + 20, wet.wetnessSup);
            yield return new WaitForSeconds(5f);
            PlayerController.moveSpeed = PlayerController.normalSpeed;
            yield break;
        }
    }
}
