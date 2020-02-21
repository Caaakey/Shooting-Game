using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryModule : MonoBehaviour
{
    public float destoryTime = .5f;
    private void Awake()
    {
        Destroy(gameObject, destoryTime);
    }
}
