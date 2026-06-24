using UnityEngine;

public class LavaZone : Weapon
{
    [SerializeField] AttackZone lavaPrefab;
    [Space]
    [SerializeField] private float tickInterval = 0.5f;
    ITargetable target;

    protected override bool TryAttack()
    {
        var lava = Instantiate(lavaPrefab);
        lava.Initialize(this);

        Vector3 spawnPos;

        target = Utility.GetNearestTarget2D(owner.Position, State.DetectRadius, owner.TargetLayerMask);
        spawnPos = target != null ? target.transform.position : Random.insideUnitCircle * State.DetectRadius;

        lava.transform.position = spawnPos;
        float radius = State.EffectRadius * 2f;
        lava.transform.localScale = new Vector3(radius, radius, 0.0f);

        return true;
    }

    protected override void OnLv2()
    {
        base.OnLv2();
        State.StretchEffectRadius(0.2f);
    }
    protected override void OnLv3()
    {
        base.OnLv3();
        State.StretchEffectRadius(0.3f);
    }
}
