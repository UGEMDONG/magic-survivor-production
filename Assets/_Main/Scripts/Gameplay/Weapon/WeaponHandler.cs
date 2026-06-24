using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour, IWeaponReceiver
{
    private IWeaponOwner owner;
    private readonly List<IWeapon> weaponList = new();
    private bool initialized;

    public IReadOnlyList<IWeapon> GetWeaponList => weaponList;

    public void Initialize(IWeaponOwner owner)
    {
        this.owner = owner;
        initialized = true;
    }

    public void ReceiveWeapon(IWeapon weapon)
    {
        weapon.Initialize(owner);
        weaponList.Add(weapon);
    }

    public IWeapon FindWeapon(string weaponName)
    {
        foreach (IWeapon weapon in weaponList)
        {
            if (weapon.WeaponName == weaponName)
                return weapon;
        }

        return null;
    }

    public bool AcquireOrUpgradeWeapon(string weaponName)
    {
        if (string.IsNullOrWhiteSpace(weaponName))
            return false;

        IWeapon ownedWeapon = FindWeapon(weaponName);

        if (ownedWeapon != null)
        {
            if (ownedWeapon.Level >= ownedWeapon.MaxLevel)
            {
                Debug.Log($"{weaponName}은(는) 이미 최대 레벨입니다.");
                return false;
            }

            ownedWeapon.Upgrade();
            return true;
        }

        return TryAddWeapon(weaponName);
    }

    public void AddWeapon(string weaponName)
    {
        TryAddWeapon(weaponName);
    }

    private bool TryAddWeapon(string weaponName)
    {
        if (!initialized)
        {
            Debug.LogWarning("WeaponHandler가 초기화되지 않았습니다.");
            return false;
        }

        if (WeaponProvider.Instance == null)
        {
            Debug.LogWarning("WeaponProvider가 존재하지 않습니다.");
            return false;
        }

        IWeapon weapon = WeaponProvider.Instance.NameToWeapon(weaponName);

        if (weapon == null)
        {
            Debug.LogWarning($"이름이 {weaponName}인 무기가 없습니다.");
            return false;
        }

        ReceiveWeapon(weapon);
        return true;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        float deltaTime = Time.deltaTime;

        foreach (IWeapon weapon in weaponList)
            weapon.Tick(deltaTime);
    }
}
