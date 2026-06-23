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
    [Header("ID")]
    [SerializeField] private string weaponName;

    // 이 공격 방식에 대한 값들은 무기(정확히는 프리팹 단위)에서 관리할지 아님 Data에서 한번에 관리할지 고민 많이 했음..
    // 무기 프리팹 인스펙터에서 관리하는 방식은 만약 이게 절대로 바뀌지 않는 값들이라면 SerializeField가 되어 있더라도
    // 내부적으로 private이며 메서드를 열어놓지 않으면 스탯에 대부분의 정보를 넘겨주는 데이타와 별개로 고정된 값으로 보고 관리할 수 있는 장점이 있고,
    // WeaponData에서 관리하면 혹시나 바꿔야 하는 상황에 대비할 수 있고, 일괄적으로 데이타를 관리할 수 있다는 장점이 있었음.
    // 만약에 간단하게나마 공격 방식이 바뀌는 상황이 올 수도 있으니 후자나 더 낫다고 판단했다.
    [Header("Attack Types")]
    [SerializeField] private WeaponAimType aimType;
    [SerializeField] private TargetSelectType targetSelectType;
    [SerializeField] private SpawnPositionType spawnPositionType;

    [Header("Normal States")]
    [SerializeField] private float coolTime;
    [SerializeField] private float damage;
    [SerializeField] private float detectRadius;
    [SerializeField] private float effectRadius;

    public string WeaponName => weaponName;

    public WeaponAimType AimType => aimType;
    public TargetSelectType TargetSelectType => targetSelectType;
    public SpawnPositionType SpawnPositionType => spawnPositionType;

    public float CoolTime => coolTime;
    public float Damage => damage;
    public float DetectRadius => detectRadius;
    public float EffectRadius => effectRadius;
}
