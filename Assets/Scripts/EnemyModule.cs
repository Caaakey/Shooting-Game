using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModule : MonoBehaviour
{
    public Transform bulletPosition;
    public GameObject bullet;
    public float speed;
    public float waitTime = 1f; //  총알이 나갈 때 까지 기다릴 시간

    private float delayTime;    //  지속적으로 업데이트 되는 시간

    //  처음 시작할 때 딱 한번만 호출되는 함수
    private void Awake()
    {
        speed = Random.Range(speed, speed * 2f);
    }

    //  매 프레임마다 호출(?)된다
    private void Update()
    {
        if (delayTime >= waitTime)
        {
            GameObject go = GameObject.Instantiate(bullet, null);
            go.transform.localPosition = bulletPosition.position;
            go.transform.localRotation = Quaternion.identity;

            //  var ?
            //  컴파일러가 자동으로 자료형을 캐치해준다
            var m = go.GetComponent<EnemyBulletModule>();
            m.Create(EnemyBulletModule.BulletType.Rotate, 0.0125f);

            delayTime = 0;
        }
        else delayTime += Time.deltaTime;
    }

    //  이동 또는 물리효과를 여기 안에다 사용해야
    //  부드럽게 보인다
    //  매 프레임마다 호출된다
    private void FixedUpdate()
    {
        transform.Translate(0, -speed, 0);
    }


}
