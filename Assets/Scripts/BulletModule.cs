using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModule : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.Translate(0, speed, 0);
    }
}
