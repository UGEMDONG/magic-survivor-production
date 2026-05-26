using UnityEngine;

public interface IDamageable
{
    public Transform transform { get; }
    public float HP { get; }
    public bool IsDie { get; }
    public void TakeDamage(float damage);
}
