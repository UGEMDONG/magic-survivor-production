using UnityEngine;

public class StraightMovement : MonoBehaviour
{
    [SerializeField] Projectile projectile;

    // Update is called once per frame
    void Update()
    {
        transform.position += projectile.Direction * projectile.Speed * Time.deltaTime;
    }
}
