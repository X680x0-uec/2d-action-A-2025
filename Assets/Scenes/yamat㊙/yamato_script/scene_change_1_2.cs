
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_change_1_2 : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // ゲーム本編のシーン名
    }
}
