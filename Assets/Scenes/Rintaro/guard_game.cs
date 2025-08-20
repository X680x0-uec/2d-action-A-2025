using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Mono.Cecil.Cil;
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

        Playermover playermover;
        GameObject obj = GameObject.Find("Player");
        playermover = obj.GetComponent<Playermover>();

        if (correct >= pushGoal)
        {
            showMessage("Success! 5秒間移動速度上昇");
            playermover.moveSpeed = 2 * playermover.normalSpeed;
            //　ここに濡れゲージ減少のコマンドを入れることもできる
            yield return new WaitForSeconds(5f);
            playermover.moveSpeed = playermover.normalSpeed;
            yield break;
        }
        else
        {
            showMessage("Failed... 5秒間移動速度低下");
            playermover.moveSpeed = 0.5f * playermover.normalSpeed;
            //　ここに濡れゲージ増加のコマンドを入れることもできる
            yield return new WaitForSeconds(5f);
            playermover.moveSpeed = playermover.normalSpeed;
            yield break;
        }
    }
}
