using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModule : MonoBehaviour
{
    public Transform bulletPosition;
    public GameObject bullet;
    public EnemyBulletModule.BulletType bulletType = EnemyBulletModule.BulletType.Default;
    public Color color;
    public int hp = 1;
    public float speed = 0.0125f;
    public float bulletSpeed = 0.02f;
    public float waitTime = 1f; //  총알이 나갈 때 까지 기다릴 시간
    public int score = 100;     //  죽으면 추가될 점수

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
        if (collision.CompareTag("Player") == false) return;

        hp--;
        if (hp > 0)
        {
            StartCoroutine(UpdateColor());
        }
        else
        {
            GameObject sfx = Instantiate(Resources.Load("Effects/SFX"), null) as GameObject;
            sfx.transform.position = transform.position;

            FindObjectOfType<GameManager>().Score = score;
            Destroy(gameObject);
        }

        Destroy(collision.gameObject);
    }

    //  0.1f 초 동안 대기
    private WaitForSeconds waitSecond = new WaitForSeconds(0.1f);
    private IEnumerator UpdateColor()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        //  new Color 는 r, g, b, a 가 모두 0 에서 1 사이값으로 넣어줘야함
        //  new Color32 는 0 ~ 255 "
        r.color = new Color32(255, 119, 0, 255);
        yield return waitSecond; // 0.1초 기다린다

        r.color = color;    //  다시 원래 컬러로 바꾸어 주고
        yield break;        //  종료
    }

}
