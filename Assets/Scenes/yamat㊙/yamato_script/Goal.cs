using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Goal : FieldObjectBase
{
    public TextMeshProUGUI scoreText;
    public RectTransform umbrellaImage; // 三角形のUI（傘）
    
public AudioSource audioSource;
public AudioClip flapSound;


    private Vector3 lastMousePosition;
    private int moveCount = 0;
    private int lastDirection = 0; // -1: 下, 1: 上, 0: 初期

    protected override IEnumerator OnAction()
    {
        showMessage("水パシャターイム！！");
        yield return new WaitForSeconds(0.5f);

        scoreText.gameObject.SetActive(true);
        umbrellaImage.gameObject.SetActive(true);

        moveCount = 0;
        lastMousePosition = Input.mousePosition;
        lastDirection = 0;

        float timer = 5f;
        while (timer > 0)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaY = currentMousePosition.y - lastMousePosition.y;

            if (Mathf.Abs(deltaY) > 30f)
            {
                int currentDirection = deltaY > 0 ? 1 : -1;

                // 傘の開閉（スケール変更）
                if (currentDirection == 1)
                {
                    // 開く（拡大）
                    umbrellaImage.localScale = new Vector3(1.5f, 1f, 1f);
                    audioSource.PlayOneShot(flapSound); // 音を再生
                }
                else if (currentDirection == -1)
                {
                    // 閉じる（縮小）
                    umbrellaImage.localScale = new Vector3(0.5f, 2f, 1f);
                }

                // 上下動のカウント
                if (currentDirection != lastDirection && lastDirection != 0)
                {
                    moveCount++;
                    lastDirection = 0;
                }
                else
                {
                    lastDirection = currentDirection;
                }

                lastMousePosition = currentMousePosition;
            }

            showMessage($"残り: {timer:F1}秒  {moveCount}パシャ");
            timer -= Time.deltaTime;
            yield return null;
        }

        showMessage("終了！");
        scoreText.text = $"総水量: {moveCount} ｍｌ";
        umbrellaImage.gameObject.SetActive(false); // 傘を非表示
        yield return new WaitForSeconds(2f);

        scoreText.gameObject.SetActive(false);
    }
}
