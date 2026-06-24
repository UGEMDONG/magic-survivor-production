using System.Collections.Generic;
using UnityEngine;

// 특정 도형(보통 원) 모양으로 바닥에 깔려 적에게 지속적으로 데미지를 주는 오브젝트 ((대표적인 예시: 모르가나 장판
// Projectile과 다르게 TriggerEnter가 아닌 TriggerStay를 쓰는 것을 볼 수 있다.
// 또한 충돌 시 파괴되는 조건도 가지고 있지 않음.
// 아마 나중에는 AttackZone이 직접 데미지를 넣지 않고 컴포넌트들만 일을 하지 않을까..
// 아 그리고 무기의 EffectRadius값에 따라 크기를 변경하는 기능도 좋다고 생각하는데, 모양이 꼭 원이 아닐 수 있고, 스케일을 한번에 바꾸는게 맞나 싶어서 보류.
public class AttackZone : AttackObject, IAttackZone
{
    List<IDamageable> targets = new();
    public IReadOnlyList<IDamageable> Targets
    {
        get
        {
            RemoveInvalidTargets();
            return targets;
        }
    }

    private void RemoveInvalidTargets()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (!IsValid(targets[i]))
                targets.RemoveAt(i);
        }
    }

    private static bool IsValid(IDamageable target)
    {
        if (target == null)
            return false;

        if (target is not Component component)
            return true;

        return component != null &&
               component.gameObject.activeInHierarchy;
    }

    protected override void Update()
    {
        base.Update();        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (weapon == null)
            return;

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        if (!targets.Contains(damageable))
            targets.Add(damageable);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (weapon == null)
            return;

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        if (targets.Contains(damageable))
            targets.Remove(damageable);
    }
}
