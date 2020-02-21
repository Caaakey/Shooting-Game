using UnityEngine;

public class BackgroundModule : MonoBehaviour
{
    public float speed = 1f;
    private float offset = 0;
    private Material material = null;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (offset > 1f) offset = 0f;

        offset += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

}