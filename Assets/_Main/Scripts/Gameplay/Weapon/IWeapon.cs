using UnityEngine;

public interface IWeapon
{
    public WeaponData BaseData { get; }
    public WeaponState State { get; }

    // 원래는 Data.weaponName(baseData)을 반환했는데, 이제는 runtimeData가 생기면서 무기가 알아서 반환하도록 하기.
    public string WeaponName => BaseData.WeaponName;

    // 프로토타입이랑 달라진 점: 이거 하나, 무기를 관리하는 클래스가 아닌 주인에 대한 인터페이스를 오너로 모시자!
    public void Initialize(IWeaponOwner owner);
    public void Tick(float deltaTime);
    public void Upgrade();
}
