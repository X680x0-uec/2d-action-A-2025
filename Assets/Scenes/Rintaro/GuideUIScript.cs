using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GuideUIScript : MonoBehaviour
{
    public Sprite mouse;
    public Sprite padButton;
    private Image guideUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guideUI = GetComponent<Image>();
    }
    void OnEnable()
    {
        if (Gamepad.current == null)
        {
            guideUI.sprite = mouse;
        } else
        {
            guideUI.sprite = padButton;
        }
    }
}
