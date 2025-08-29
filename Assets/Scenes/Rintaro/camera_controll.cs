using UnityEngine;

public class camera_controll : MonoBehaviour
{
    [SerializeField] Transform playerTf;
    Vector3 plpos;
    [SerializeField] float compX;
    [SerializeField] PlayerController pl;
    //主人公に対してどれだけカメラがずれるか
    void Update()
    {
        plpos = playerTf.position;
        if (pl.flag == 0) {
            transform.position = new Vector3(plpos.x+compX,transform.position.y,-26);
        }
    }
}
