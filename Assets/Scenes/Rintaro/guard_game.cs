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

    protected override IEnumerator OnAction()
    {
        isActioned = true;
        correct = 0;
        failed = 0;
        gage_canvas.gameObject.SetActive(true);
        showMessage("Push Space on green area! Press space");
        //スペースキーを押したら開始っていうことをしたい
        yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
        //ここまでそれ
        StartCoroutine(targets.Move());

        yield return new WaitUntil(() => targets.finished || !isContacted);

        PlayerController PlayerController;
        GameObject obj = GameObject.Find("Actor");
        PlayerController = obj.GetComponent<PlayerController>();
        WetnessCounter wet = obj.GetComponent<WetnessCounter>();  // 濡れゲージ取得

        if (correct >= pushGoal)
        {
            showMessage("Success! 服が乾いて身軽になった気がする");
            PlayerController.moveSpeed = 2 * PlayerController.normalSpeed;
            wet.wetness -= 20;
            yield return new WaitForSeconds(5f);
            PlayerController.moveSpeed = PlayerController.normalSpeed;
            yield break;
        }
        else
        {
            showMessage("Failed... びちょ濡れで足取りが重くなった");
            PlayerController.moveSpeed = 0.5f * PlayerController.normalSpeed;
            wet.wetness += 20;
            yield return new WaitForSeconds(5f);
            PlayerController.moveSpeed = PlayerController.normalSpeed;
            yield break;
        }
    }
}
