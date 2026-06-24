using UnityEngine;

public class Fire : AttackObject
{
    Vector3 direction;
    // [SerializeField] Animation animation;

    public void AnimEvent_OnBurn()
    {
        attackCollider.enabled = true;
    }
    public void AnimEvent_OnEnd()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(weapon.State.Damage);
    }
    /*protected override void Start()
    {
        base.Start();
        animation.Play();
    }*/
}
