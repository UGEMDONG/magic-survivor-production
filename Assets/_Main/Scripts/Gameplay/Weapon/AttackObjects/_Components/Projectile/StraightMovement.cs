using UnityEngine;

// 작명이 살짝 햇갈리는데 => 주인 투사체의 정해진 방향으로 앞으로 가는 움직임이다.
public class StraightMovement : MonoBehaviour
{
    IProjectile projectile;

    private void Awake()
    {
        projectile = GetComponent<IProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = projectile.Direction;
        transform.position += projectile.Direction * projectile.Speed * Time.deltaTime;
    }
}
