using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    [SerializeField] float destroyTime = 0.5f;

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(DestroySelf), destroyTime);
    }
}
