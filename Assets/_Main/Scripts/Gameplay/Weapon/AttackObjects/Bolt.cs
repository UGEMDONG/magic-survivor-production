using UnityEngine;

// 초기화한 IDamageable을 향해 유도되는 공격 오브젝트,
// 하지만 나중에 투사체나, 유도 미사일 기능 재사용을 위해 AttackObject를 상속받는 Projectile 등을 구현하는게 좋을듯.
public class Bolt : Projectile
{
    IDamageable target;

    public void Initialize(IWeapon owner, IDamageable target)
    {
        base.Initialize(owner);
        this.target = target;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 dir = Utility.GetNormalizedDir(target.transform.position, transform.position);
        transform.position += dir * Time.deltaTime * speed;
    }
}
