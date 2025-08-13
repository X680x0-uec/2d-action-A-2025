using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MessageCharactor : FieldObjectBase
{
    [SerializeField] private List<string> messages;
    [SerializeField] private Collider2D objectExistence;

    private bool notContacted = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        notContacted = other.gameObject.CompareTag("Player");
    }

    //  親クラスから呼ばれるコールバックメソッド（接触時に実行）
    protected override IEnumerator OnAction()
    {
        for (int i = 0; i < messages.Count; ++i)
        {
            yield return null;

            showMessage(messages[i]);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            //if (notContacted)
            //{
            //    yield break;
            //}
            
            if (i == 10) //messages.Count && Input.GetKeyDown(KeyCode.Space))
            {
                Playermover playermover;
                GameObject obj = GameObject.Find("Player");
                playermover = obj.GetComponent<Playermover>();
                playermover.moveSpeed = 8f;
                yield return new WaitForSeconds(5f);
                yield break;
            }
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
