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
    [SerializeField] private string messages;
    [SerializeField] private int pushGoal;
    //ガードレール→かんかんってするのがリズムゲーっぽい
    //タイミングよくスペースキーを押すのを繰り返すとおーけー
    //複数ミスまでオーケー　難しくね？押したら開始とかどうすか
    protected override IEnumerator OnAction()
    {
        isActioned = true;
        int i = 0;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || !isContacted);
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

        Playermover playermover;
        GameObject obj = GameObject.Find("Player");
        playermover = obj.GetComponent<Playermover>();

        if (i == pushGoal)
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
