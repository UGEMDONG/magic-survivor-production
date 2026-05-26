using System.Collections.Generic;
using UnityEngine;

public interface IWeaponProvider
{
    public IReadOnlyList<IWeapon> GetWeaponList { get; }
    public IWeapon NameToWeapon(string weaponName);
}
