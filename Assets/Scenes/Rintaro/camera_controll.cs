using UnityEngine;

public class camera_controll : MonoBehaviour
{
    [SerializeField] Transform playerTf;
    Vector3 plpos;
    [SerializeField] float compX;
    //主人公に対してどれだけカメラがずれるか
    void Update()
    {
        plpos = playerTf.position;
        transform.position = new Vector3(plpos.x+compX,-5,-26);
    }
}
