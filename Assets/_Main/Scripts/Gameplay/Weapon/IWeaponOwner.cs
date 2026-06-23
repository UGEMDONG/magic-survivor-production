using UnityEngine;

// IWeaponOwner는 말그대로 무기를 가지는(보통 이걸 상속받거나 가지는 객체는 WeaponHandler를 쓸듯) 객체의 인터페이스
// 왜 무기를 가지는데 무기 리스트를 반환 안해?? 라는 궁금증이 생길 수 있다.
// 보통은 Owner가 Receiver를 가지고 알아서 관리하게 하기 때문에 아직 필요성은 못느낌
// 만약 구현한다면 WeaponList => holder.WeaponList 같이 만들 수 있을듯
public interface IWeaponOwner
{
    // 이건 아마 플레이어나 적 등의 능력치가 구현되면 넣지 않을까
    // public Status Status { get; }

    // 무기가 공격 기준과 방향을 잘 찾을 수 있게 하는 프로퍼티
    public Transform transform { get; }
    public Vector3 Position => transform.position;

    public LayerMask TargetLayerMask { get; }
    public Vector3 MoveDirection { get; }
}
