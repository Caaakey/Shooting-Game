using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver = null;
    public Text scoreText = null;

    public GameObject enemy = null;

    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score += value;
            scoreText.text = "<b>Score</b>\n" + score.ToString();
        }
    }

    private void Awake()
    {
        Score = 0;
        StartCoroutine(StartGame());
    }

    public void SetGameOver()
    {
        gameOver.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyModule>();
        if (enemy != null) enemy.IsShooting = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

    private IEnumerator StartGame()
    {
        float waitTime = Time.time + Random.Range(0.2f, 1.5f);
        while (true)
        {
            if (Time.time >= waitTime)
            {
                Vector3 position = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f);
                GameObject e = Instantiate(enemy, null);
                e.transform.position = position;

                EnemyModule module = e.GetComponent<EnemyModule>();
                module.speed = Random.Range(0.0125f, 0.025f);
                module.bulletSpeed = Random.Range(0.0125f, 0.03f);
                module.hp = Random.Range(0, 5);

                module.bulletType = (EnemyBulletModule.BulletType)Random.Range(0, 2);

                if (module.hp >= 2)
                {
                    module.score = module.hp * 100;
                }

                waitTime = Time.time + Random.Range(0.2f, 1.5f);
            }

            yield return null;
        }   
    }
}
