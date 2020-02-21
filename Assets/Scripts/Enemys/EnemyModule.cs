using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModule : MonoBehaviour
{
    public Transform bulletPosition;
    public GameObject bullet;
    public float bulletSpeed = 0.0125f;
    public EnemyBulletModule.BulletType bulletType = EnemyBulletModule.BulletType.Default;
    public Color color;
    public int hp = 1;
    public float speed;
    public float waitTime = 1f; //  총알이 나갈 때 까지 기다릴 시간

    private float delayTime;    //  지속적으로 업데이트 되는 시간
    public bool IsShooting { get; set; } = false;

    //  처음 시작할 때 딱 한번만 호출되는 함수
    private void Awake()
    {
        speed = Random.Range(speed, speed * 2f);

        if (!color.Equals(Color.white))
            GetComponent<SpriteRenderer>().color = color;
    }

    //  매 프레임마다 호출(?)된다
    private void Update()
    {
        if (!IsShooting) return;

        if (delayTime >= waitTime)
        {
            GameObject go = GameObject.Instantiate(bullet, null);
            go.transform.localPosition = bulletPosition.position;
            go.transform.localRotation = Quaternion.identity;

            //  var ?
            //  컴파일러가 자동으로 자료형을 캐치해준다
            var m = go.GetComponent<EnemyBulletModule>();
            m.Create(bulletType, bulletSpeed);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        hp--;
        if (hp > 0)
        {
            StartCoroutine(UpdateColor());
        }
        else
        {
            GameObject sfx = Instantiate(Resources.Load("Effects/Enemy Death Effect") as Object, null) as GameObject;
            sfx.transform.position = transform.position;

            Destroy(gameObject);
        }

        Destroy(collision.gameObject);
    }

    private WaitForSeconds waitSecond = new WaitForSeconds(0.1f);
    private IEnumerator UpdateColor()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        r.color = new Color32(255, 119, 0, 255);
        yield return waitSecond;

        r.color = color;
        yield break;
    }

}
