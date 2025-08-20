using System.Collections;
using UnityEngine;

public class bar_script : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float limit;
    [SerializeField] guard_game score;
    [SerializeField] Canvas targetc;
    [SerializeField] RectTransform rec;
    public bool finished = false;
    bool pushed = false;
    bool started = false;

    public IEnumerator Move()
    {
        targetc.gameObject.SetActive(true);
        Debug.Log("started");
        rec.anchoredPosition = new Vector2(-limit, 0);
        //anchoredPosition: Canvas内でのpositionのこと
        StartCoroutine(Moveroop());
        started = true;
        yield return new WaitUntil(() => (score.failed + score.correct) == score.challengeCount || !score.isContacted);
        StopCoroutine(Moveroop());
        finished = true;
        targetc.gameObject.SetActive(false);
    }
    IEnumerator Moveroop()
    {
        //無限ループ処理　while (true) {} でもいいけどフリーズが怖くてこうしました
        for (int i = 0; i <= 100000; i++)
        {
            rec.anchoredPosition += Vector2.right * speed * Time.deltaTime;
            rec.anchoredPosition = new Vector2(Mathf.Clamp(rec.anchoredPosition.x, -limit, limit), 0);
            if (rec.anchoredPosition.x >= limit)
            {
                pushed = false;
                rec.anchoredPosition = new Vector2(-limit, 0);
                score.failed += 1;
                score.showMessage("oops...");
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && started)
        {
            Debug.Log("spaced");
            if (0.4f <= rec.anchoredPosition.x || rec.anchoredPosition.x <= -0.72f)
            {
                if (!pushed)
                {
                    score.correct += 1;
                    score.showMessage("great!");
                }
                else
                {
                    score.failed += 1;
                    score.showMessage("oops...");
                }
            }
            else
            {
                score.failed += 1;
                score.showMessage("oops...");
            }
            rec.anchoredPosition = new Vector2(-limit, 0);
            pushed = false;
        }
    }





}
