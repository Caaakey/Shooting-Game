using UnityEngine;

class DestroyModule : MonoBehaviour
{
    public float destroyTime = .5f;
    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }

}
