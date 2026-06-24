using UnityEngine;

// 처음에 정해진 방향으로 바뀌지 않고 앞으로 간다.
public class StartDirMove : MonoBehaviour
{
    [SerializeField] float speedMin = 3f;
    [SerializeField] float speedMax = 6f;
    float speed;
    Vector3 dir;

    private void Awake()
    {
        speed = Random.Range(speedMin, speedMax);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
