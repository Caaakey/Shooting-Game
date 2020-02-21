using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

}
