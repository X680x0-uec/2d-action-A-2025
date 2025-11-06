using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class KurukuruMinigame : FieldObjectBase
{
    public GameObject KurukuruKasa;
    [SerializeField] GameUIManager GUM;
    [SerializeField] int scoreAmount;
    private bool Kuru = true;
    protected override IEnumerator OnAction()
    {
        KurukuruKasa.gameObject.SetActive(true);

        isActioned = true;

        PlayerController PlayerController;
        GameObject obj = GameObject.Find("Actor");
        PlayerController = obj.GetComponent<PlayerController>();
        WetnessCounter wet = obj.GetComponent<WetnessCounter>();

        yield return null;

        showMessage("マウス/右スティックを時計回りに回せ！");

        yield return null;

        while (KurukuruKasa.activeSelf == true)
        {
            yield return null;

            if (!isContacted)
            {
                Kuru = false;

                KurukuruKasa.gameObject.SetActive(false);
            }
        }

        //yield return new WaitUntil(() => KurukuruKasa.activeSelf == false);

            if (Kuru == true)
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
