using UnityEngine;

public class ScaleByTime : MonoBehaviour
{
    [SerializeField] float increaseSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.x += increaseSpeed * Time.deltaTime;
        newScale.y += increaseSpeed * Time.deltaTime;
        newScale.z = 1f;
        transform.localScale = newScale;
    }
}
