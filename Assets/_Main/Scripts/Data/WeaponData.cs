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
    // 이 값들을 모두 private으로 만든 이유:
    // 우선 WeaponData를 무기들이 모두 getter로 반환할 건데, 그러면 자연스레 ~.damage 같이 접근하고 설정 가능하다.
    // 그럼 WeaponData를 반환하는 쪽에서 readonly로 쓰면 안되냐? 애초에 프로퍼티는 변수가 아닌 메서드에 가까워서 readonly를 못붙힌다..
    [SerializeField] private string weaponName;
    [SerializeField] private WeaponDirectionType directionType;
    [SerializeField] private LayerMask targetLayers;
    [Space]
    [SerializeField] private float coolTime;
    [SerializeField] private float damage;
    [SerializeField] private float detectRadius;
    [SerializeField] private float effectRadius;

    public string WeaponName => weaponName;
    public WeaponDirectionType DirectionType => directionType;
    public LayerMask TargetLayers => targetLayers;

    public float CoolTime => coolTime;
    public float Damage => damage;
    public float DetectRadius => detectRadius;
    public float EffectRadius => effectRadius;
}
