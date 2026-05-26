using System.Collections.Generic;
using UnityEngine;

public interface IWeaponReceiver
{
    public IReadOnlyList<IWeapon> GetWeaponList { get; }

    // 아까 무기가 오너를 갖기 때문에 무기들을 한번에 초기화시켜주기 위해서 암묵적으로 가져야 한다는 메서드
    public void Initialize(IWeaponOwner owner);
    void ReceiveWeapon(IWeapon weapon);
}
