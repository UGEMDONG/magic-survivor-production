using UnityEngine;

public class HomingMovement : MonoBehaviour
{
    IProjectile projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        projectile = GetComponent<IProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform targetTr = projectile.Target.transform;
        Vector3 dir = Utility.GetNormalizedDir(targetTr.position, transform.position);
        transform.position += dir * Time.deltaTime * projectile.Speed;
    }
}
