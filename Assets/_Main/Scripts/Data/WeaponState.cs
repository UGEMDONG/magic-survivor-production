using UnityEngine;

// 왜 이렇게 만들기 노가다인 데이타용 클래스를 추가했냐?
// 무기의 데이타와 WeaponState를 나눈 이유 자체가 기존 방식에선 Data만 들고 있고
// 사실상 같은 무기면 모두 똑같은 프리펩 ScriptableObject를 참조하고 있음.
// ScriptableObject는 값을 바꾼다고 복사본이 자동으로 생기는 것도 아니고 원본 훼손에 대한 방지도 엔진에선 딱히 안함.
// 그리고 결정적으로 '바뀌지 않는 기존 데이타'가 필요하고 '게임 시에 무기마다 다르게 바뀌는 정보'도 필요함
// 그래서 WeaponData는 '처음의 표본 값'이 되고, WeaponState은 무기마다 다른 '변화되는 능력치'가 된다.
public class WeaponState
{
    // 왜 하나하나 프로퍼티로 하냐? 일반 변수는 get과 set의 보호 수준을 다르게 설정 못함.
    // 아무리 WeaponState라고 해도 일단은 맘대로 값을 바꿀 수 없음. 안전하고 명시적이게 특정한 메서드를 사용하자!
    public float Damage { get; private set; }
    public float CoolTime { get; private set; }
    public WeaponDirectionType DirectionType { get; private set; }
    public LayerMask TargetLayers { get; private set; }
    public float DetectRadius { get; private set; }
    public float EffectRadius { get; private set; }

    // 이렇게 무기와 의미가 똑같은 값들을 프로퍼티에 하나하나 할당한다.
    // 이 프로퍼티들은 객체 내에서 사실상 변수처럼 사용된다.
    public WeaponState(WeaponData baseData)
    {
        Damage = baseData.Damage;
        CoolTime = baseData.CoolTime;
        DirectionType = baseData.DirectionType;
        TargetLayers = baseData.TargetLayers;
        DetectRadius = baseData.DetectRadius;
        EffectRadius = baseData.EffectRadius;
    }

    // 아직은 업그레이드 기능이 미완성이기에 사용할 일이 많지 않고 불완정하지만
    // 이런 예시를 통해 값마다 의미와 형태에 맞는 메서드를 사용해야 한다는 것을 알 수 있다.
    public void AddDamage(float add)
    {
        Damage += add;
    }
    public void ReduceCoolTime(float reduce)
    {
        if (reduce >= CoolTime) return;
        CoolTime -= reduce;
    }
    public void StretchEffectRadius(float add)
    {
        EffectRadius += add;
    }
    //AddTargetLayer(int layer), SetTargetLayer(Layermask mask) 등등등..
}
