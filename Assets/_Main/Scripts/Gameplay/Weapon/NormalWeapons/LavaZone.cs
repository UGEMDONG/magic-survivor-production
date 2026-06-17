using UnityEngine;

public class LavaZone : Weapon
{
    [SerializeField] AttackZone lavaPrefab;
    ITargetable target;

    protected override bool TryAttack()
    {
        var lava = Instantiate(lavaPrefab);
        lava.Initialize(this);

        Vector3 spawnPos;

        target = Utility.GetNearestTarget2D(owner.Position, State.DetectRadius, State.TargetLayers);
        spawnPos = target != null ? target.transform.position : Random.insideUnitCircle * State.DetectRadius;

        lava.transform.position = spawnPos;
        
        return true;
    }
}
