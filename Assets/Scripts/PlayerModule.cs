using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModule : MonoBehaviour
{
    public Text countText = null;

    public int count = 3;       //  플레이어 목숨
    public float speed;         //  속도
    public Vector2 boundary;    //  플레이어 위치 제한값

    public Transform bulletTransform;    //  총알 생성할 자리
    public GameObject bullet;           //  총알

    public bool isDeath = false;
    public bool isRestore = false;
    public SpriteRenderer sprite = null;

    private void Awake()
    {
        countText.text = "x " + count.ToString();
    }

    void Update()
    {
        if (isDeath) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bullet, null);

            go.transform.localPosition =
                bulletTransform.position;
        }
    }

    private void FixedUpdate()
    {
        if (isDeath) return;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeath || isRestore) return;
        if (!collision.CompareTag("Enemy")) return;

        GameObject sfx = Instantiate(Resources.Load("Effects/SFX"), null) as GameObject;
        sfx.transform.position = transform.position;

        sprite.color = new Color(1, 1, 1, 0);
        Destroy(collision.gameObject);
        isDeath = true;

        StartCoroutine(UpdateAlpha());
    }

    private WaitForSeconds waitSecond = new WaitForSeconds(0.05f);
    private IEnumerator UpdateAlpha()
    {
        yield return new WaitForSeconds(1.5f);
        count--;
        countText.text = "x " + count.ToString();

        if (count <= 0)
        {
            FindObjectOfType<GameManager>().SetGameOver();
            yield break;
        }

        isDeath = false;
        isRestore = true;
        float fixedTime = Time.time;
        while (Time.time < fixedTime + 1.5f)
        {
            if (sprite.color.a == 1f) sprite.color = new Color(1, 1, 1, .35f);
            else sprite.color = Color.white;

            yield return waitSecond;
        }

        isRestore = false;
        sprite.color = Color.white;
        yield break;
    }

}
