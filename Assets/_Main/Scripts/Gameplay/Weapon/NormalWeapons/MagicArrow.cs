using UnityEngine;

public class MagicArrow : Weapon
{
    [SerializeField] Projectile arrowPrefab;

    protected override bool TryAttack()
    {
        Vector3 dir = GetAimDir();
        if(dir == Vector3.zero) return false;

        var arrow = Instantiate(arrowPrefab);
        arrow.Initialize(this, 5f, dir, null);

        arrow.transform.position = transform.position;
        return true;
    }
}
