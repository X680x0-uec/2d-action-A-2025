using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class guard_game : FieldObjectBase
{
    [SerializeField] private int pushGoal;
    //目標クリア回数
    [SerializeField] public int correct;
    //成功回数
    [SerializeField] public int failed;
    //失敗回数
    [SerializeField] public int challengeCount;
    //挑戦回数
    [SerializeField] bar_script targets;
    [SerializeField] Canvas gage_canvas;
    [SerializeField] GameUIManager GUM;
    [SerializeField] int scoreAmount;

    protected override IEnumerator OnAction()
    {
        isActioned = true;
        correct = 0;
        failed = 0;
        gage_canvas.gameObject.SetActive(true);
        showMessage("タイミングよくクリックせよ！クリックで開始");
        //左クリックで開始っていうことをしたい
    
yield return new WaitUntil(() => !Input.GetMouseButton(0));
yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
yield return new WaitUntil(() => !Input.GetMouseButton(0));

        //ここまでそれ
        StartCoroutine(targets.Move(this));

        yield return new WaitUntil(() => targets.finished || !isContacted);

        PlayerController PlayerController;
        GameObject obj = GameObject.Find("Actor");
        PlayerController = obj.GetComponent<PlayerController>();
        WetnessCounter wet = obj.GetComponent<WetnessCounter>();  // 濡れゲージ取得

        if (correct >= pushGoal)
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
            wet.wetness = Mathf.Min(wet.wetness + 20, wet.wetnessSup);
            yield return new WaitForSeconds(5f);
            PlayerController.moveSpeed = PlayerController.normalSpeed;
            yield break;
        }
    }
}
