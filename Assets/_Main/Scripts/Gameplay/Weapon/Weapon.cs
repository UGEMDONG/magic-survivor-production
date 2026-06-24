using UnityEngine;

// 이전에서 이름 조금 바꾸고, 메서드 몇 가지 수정함.
public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected /*readonly*/ WeaponData baseData;
    protected WeaponState state;
    protected IWeaponOwner owner;

    protected float coolTimer;
    protected int level = 1;

    public WeaponData BaseData => baseData;
    public WeaponState State => state;

    protected abstract bool TryAttack();

    // GetFixedDir랑 GetTargetDir는 abstract도 좋다고 생각하는데, 안 쓸 무기 클래스에서 구현부를 꼭 해야하는게 귀찮아서 대충이라도 넣어봄.
    protected virtual Vector2 GetFixedDir() => Vector2.up;
    protected virtual Vector2 GetTargetDir() => Vector2.zero;

    protected virtual Vector2 GetAimDir()
    {
        return state.AimType switch
        {
            WeaponAimType.MoveDirection =>
                owner.MoveDirection.normalized,

            WeaponAimType.TargetDirection  =>
                GetTargetDir(),

            WeaponAimType.FixedDirection =>
               GetFixedDir(),

            _ => Vector2.zero
        };
    }

    // 아까 오너가 Transform을 반환하기 때문에 인자값이 클래스가 아니여도 자식으로 쉽게 들어갈 수 있음.
    public virtual void Initialize(IWeaponOwner owner)
    {
        state = new WeaponState(BaseData);

        this.owner = owner;
        transform.SetParent(owner.transform);
        transform.localPosition = Vector3.zero;
    }
    // 쿨타임 방식 개선
    public virtual void Tick(float deltaTime)
    {
        if (coolTimer < State.CoolTime)
            coolTimer += deltaTime;

        if (coolTimer >= State.CoolTime)
        {
            if (!TryAttack()) return;
            coolTimer = 0f;
        }
    }

    // 먼가 딱봐도 겁나 비효율적인 것 같긴 한데, 우리 게임 일단 3레벨이 Max로 정했으니 이렇게 한겨
    protected virtual void OnLv2() { State.AddDamage(State.Damage); }// 뎀지 2배!
    protected virtual void OnLv3() { State.ReduceCoolTime(State.CoolTime / 2f); }// 쿨타임 2배 단축!!
    public virtual void Upgrade()
    {
        if (level >= 3)
        {
            Debug.Log("무기 " + BaseData.WeaponName + " 이 이미 최대레벨입니다!");
            return;
        }
        level++;
        switch (level)
        {
            case 2: OnLv2(); break;
            case 3: OnLv3(); break;
        }
    }    
}
