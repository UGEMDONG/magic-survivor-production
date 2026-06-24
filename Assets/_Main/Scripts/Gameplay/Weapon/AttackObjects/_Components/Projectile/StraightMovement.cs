using UnityEngine;

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
        transform.position += projectile.Direction * projectile.Speed * Time.deltaTime;
    }
}
