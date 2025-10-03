using JetBrains.Annotations;
using UnityEngine;

public class MouseMoveTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Rigidbody2D MyRb;
    Vector2 mousePos;
    [SerializeField] Camera camera;
    private int flag = 0;
    private int i = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        MyRb.rotation = angle;

        switch (flag)
        {
            case 1:

                if (angle > 0 && angle < 90)
                    flag++;
                Debug.Log("flag 2 yade");
                break;

            case 2:

                if (angle > -90 && angle < 0)
                    flag++;
                Debug.Log("flag 3 yade");
                break;

            case 3:

                if (angle > -180 && angle < -90)
                    flag++;
                Debug.Log("flag 4 yade");
                break;

            case 4:

                if (angle > -270 && angle < -180)
                {
                    if (i < 10)
                    {
                        i++;
                        flag = 1;
                        Debug.Log("flag 1 yade");
                    }
                    else
                    {
                        flag = 0;
                        Debug.Log("flag 0 yade");
                        this.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }
}
