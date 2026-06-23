using UnityEngine;

// 기존 AttackObject의 도메인 설계가 너무 방대해서 조금 잘못 되었다고 생각해서 따로 상속받은 클래스로 분리하였다.
// 지금은 없앴지만 시행착오 당시 이 클래스를 상속받은 Misile 등으로 다시 상속시켜 추상이 아닌 클래스로 생성하여 사용하는 방식을 떠올렸었지만
// 민섭선배의 간단한 면담 이후 상속 구조를 늘리지 말고 컴포넌트를 조립하자는 생각이 크게 들었다.
// 애초에 Misile 구조를 포기한 이유가 그렇게 세분화 할거면 컴포넌트가 따로 필요가 없고, 초기화만 어려워지기 때문이다.
public class Projectile : AttackObject, IProjectile
{
    [SerializeField] private bool destroyOnAttack = true;
    [SerializeField] private bool hitOnlyTarget = true;

    protected float speed;
    protected Vector3 direction;
    protected ITargetable target;

    public float Speed => speed;
    public Vector3 Direction => direction;
    public ITargetable Target => target;

    public void Initialize(IWeapon owner, float speed, ITargetable target = null)
    {
        base.Initialize(owner);
        this.speed = speed;
        this.target = target;
    }
    public void Initialize(IWeapon owner, float speed, Vector3 direction, ITargetable target = null)
    {
        base.Initialize(owner);
        this.speed = speed;
        this.direction = direction;
        this.target = target;
    }

    protected virtual void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider == null)
            return;

        if (weapon == null)
            return;        

        IDamageable damageable = hitCollider.GetComponent<IDamageable>();        

        if (damageable == null)
            return;

        if (target != null && hitOnlyTarget &&
            target.gameObject != damageable.gameObject)
            return;

        damageable.TakeDamage(weapon.State.Damage);

        if (destroyOnAttack)
            Destroy(gameObject);
    }   
}
