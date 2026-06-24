using UnityEngine;

// 처음에 정해진 방향으로 바뀌지 않고 앞으로 간다.
public class StartDirMovement : MonoBehaviour
{
    IProjectile projectile;
    Vector3 dir;

    private void Awake()
    {
        projectile = GetComponent<IProjectile>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * projectile.Speed * Time.deltaTime;
    }
}
