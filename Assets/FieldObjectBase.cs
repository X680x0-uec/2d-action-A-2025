using System.Buffers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//「abstract」抽象クラス・・・「抽象メソッド」という中身の処理が記述されていないメソッドを持つクラスのこと。
//これを持つクラスは「親クラス」となり、サブクラス（子クラス）に継承、利用される。詳しくは検索してくれ。
public abstract class FieldObjectBase : MonoBehaviour
{
    //UnityのUI上で作ったオブジェクトをバインドする。
    public Canvas window;
    public Text target;

    //接触判定
    public bool isContacted = false;
    public bool isActioned = false;
    private IEnumerator coroutine;

    //colliderをもつオブジェクトの領域に入ったとき
    private void OnTriggerEnter2D(Collider2D other)
    {
        isContacted = other.gameObject.CompareTag("walker");
    }

    //colliderをもつオブジェクトの領域外にでたとき
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isContacted) // 雨が判定から「出た」とき、ここにtrueが入るのを防止する
        {
            isContacted = !other.gameObject.CompareTag("walker");
        }
    }

    private void FixedUpdate()
    {
        if (isContacted && !isActioned && coroutine == null && Input.GetKeyDown(KeyCode.Space))
        {
            coroutine = CreateCoroutine();
            //コルーチンの起動
            StartCoroutine(coroutine);
        }
    }

    //リアクション用コルーチン
    private IEnumerator CreateCoroutine()
    {
        //window起動
        window.gameObject.SetActive(true);

        //抽象メソッド呼び出し
        yield return OnAction();

        //window終了
        this.target.text = "";
        this.window.gameObject.SetActive(false);

        //while (isContacted)
        //{
        //    yield return null;
        //}

        //yield return new WaitForSeconds(5f);

        StopCoroutine(coroutine);
        coroutine = null;
    }

    protected abstract IEnumerator OnAction();

    //メッセージを表示する
    public void showMessage(string message)
    {
        this.target.text = message;
    }

}
