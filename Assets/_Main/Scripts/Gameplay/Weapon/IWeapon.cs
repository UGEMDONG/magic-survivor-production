using UnityEngine;

public interface IWeapon
{
    public WeaponData Data { get; }
    // public Vector3 Position { get; }
    public string WeaponName => Data.weaponName;

    // 저번이랑 달라진 점: 이거 하나, 무기를 관리하는 클래스가 아닌 주인에 대한 인터페이스를 오너로 모시자!
    public void Initialize(IWeaponOwner owner);
    public void Tick(float deltaTime);
    public void Upgrade();
}
