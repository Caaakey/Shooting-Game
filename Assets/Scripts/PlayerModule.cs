using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModule : MonoBehaviour
{
    public float speed;         //  속도
    public Vector2 boundary;    //  플레이어 위치 제한값

    public Transform bulletTransform;    //  총알 생성할 자리
    public GameObject bullet;           //  총알

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bullet, null);

            go.transform.localPosition =
                bulletTransform.position;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localPosition.y < boundary.y)
                transform.Translate(0, speed, 0);

            else RecoveryPosition();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.localPosition.y > -boundary.y)
                transform.Translate(0, -speed, 0);

            else RecoveryPosition();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.localPosition.x > -boundary.x)
                transform.Translate(-speed, 0, 0);

            else RecoveryPosition();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.localPosition.x < boundary.x)
                transform.Translate(speed, 0, 0);

            else RecoveryPosition();
        }

    }

    private void RecoveryPosition()
    {
        transform.localPosition =
                    new Vector3(
                        transform.localPosition.x,
                        transform.localPosition.y);
    }

}
