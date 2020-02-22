using System;
using System.Collections.Generic;

using UnityEngine;

public class EnemyBulletModule : MonoBehaviour
{
    public enum BulletType
    {
        Default = 0,
        Guided,
        Rotate
    }

    private BulletType type;
    private float speed;
    
    public void Create(BulletType type, float bulletSpeed)
    {
        this.type = type;
        speed = bulletSpeed;

        switch (type)
        {
            case BulletType.Guided:
            case BulletType.Rotate:
                PlayerModule p = FindObjectOfType<PlayerModule>();
                if (p == null) break;

                float angle = GetAngle(p.transform.position,
                                       transform.position);

                transform.rotation = Quaternion.Euler(0, 0, angle);
                return;

            case BulletType.Default: break;
        }

        transform.Rotate(0, 0, 180);
    }

    private void FixedUpdate()
    {
        if (type == BulletType.Rotate)
            GuidMissle();

        transform.Translate(0, speed, 0);
    }

    private float delayTime = 0;
    private float waitTime = .5f;
    private void GuidMissle()
    {
        if (delayTime >= waitTime)
        {
            var p = FindObjectOfType<PlayerModule>();
            if (p != null)
            {
                float angle = GetAngle(p.transform.position, transform.position);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            delayTime = 0;
        }
        else delayTime += Time.deltaTime;
    }

    public float GetAngle(Vector2 to, Vector2 from)
    {
        Vector2 dir = (to - from).normalized;
        float angle = Mathf.Atan2(dir.x, dir.y);
        float degree = (angle * 180) / Mathf.PI;

        if (degree < 0) degree += 360;

        return 360 - degree;
    }

}
