using UnityEngine;

public enum WeaponDirectionType
{
    None,               // 방향이 필요 없는 무기
    MoveDirection,      // 이동 방향 기준
    TargetDirection,    // 무기가 선택한 타겟 방향 기준 ((선택한 방향은 꼭 타게팅이란건 아님.
    FixedDirection      // 고정 방향 기준               ((이거도 꼭 영원히 한방향이란건 아님.
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponDirectionType directionType;
    public LayerMask targetLayers;  // 물리적으로 감지 가능한 레이어 집합
    [Space]
    public float coolTime;
    [Space]
    public float damage;
    public float attackRadius;      // 공격 범위(탐지 범위)
    public float attackRange;       // 공격 효과 넓이(마법 장판 반지름 등)
}
