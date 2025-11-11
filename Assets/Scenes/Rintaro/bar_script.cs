using System.Collections;
using UnityEngine;

public class bar_script : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float limit;
    public guard_game score;
    [SerializeField] Canvas targetc;
    [SerializeField] RectTransform rec;
    public bool finished = false;
    bool pushed = false;
    bool started = false;

    public IEnumerator Move(guard_game gm)
    {
        finished = false;
        score = gm;
        targetc.gameObject.SetActive(true);
        rec.anchoredPosition = new Vector2(-limit, 0);
        //anchoredPosition: Canvas内でのpositionのこと
        StartCoroutine(Moveroop());
        started = true;
        yield return new WaitUntil(() => (score.failed + score.correct) == score.challengeCount || !score.isContacted);
        finished = true;
        started = false;
        targetc.gameObject.SetActive(false);
    }
    IEnumerator Moveroop()
    {
        //無限ループ処理
        while (!finished)
        {
            rec.anchoredPosition += Vector2.right * speed * Time.deltaTime;
            rec.anchoredPosition = new Vector2(Mathf.Clamp(rec.anchoredPosition.x, -limit, limit), 0);
            if (rec.anchoredPosition.x >= limit)
            {
                pushed = false;
                rec.anchoredPosition = new Vector2(-limit, 0);
                score.failed += 1;
                score.showMessage("ミス...");
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && started && !finished)
        {
            if (0.4f <= rec.anchoredPosition.x)
            {
                if (!pushed)
                {
                    score.correct += 1;
                    score.showMessage("OK!");
                }
                else
                {
                    score.failed += 1;
                    score.showMessage("ミス...");
                }
            }
            else
            {
                score.failed += 1;
                score.showMessage("ミス...");
            }
            rec.anchoredPosition = new Vector2(-limit, 0);
            pushed = false;
        }
    }





}
