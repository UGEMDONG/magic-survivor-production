using System.Collections.Generic;
using UnityEngine;

public interface IAttackZone
{
    public IWeapon Weapon { get; }
    public IReadOnlyList<IDamageable> Targets { get; }
}
