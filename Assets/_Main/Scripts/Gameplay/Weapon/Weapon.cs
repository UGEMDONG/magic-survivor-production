using UnityEngine;

// 이전에서 이름 조금 바꾸고, 메서드 몇 가지 수정함.
public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected WeaponData baseData;
    protected IWeaponOwner owner;

    protected float coolTimer;
    protected int level;

    public WeaponData Data => baseData;

    protected abstract bool TryAttack();

    // GetFixedDir랑 GetTargetDir는 abstract도 좋다고 생각하는데, 안 쓸 무기 클래스에서 구현부를 꼭 해야하는게 귀찮아서 대충이라도 넣어봄.
    protected virtual Vector2 GetFixedDir() => Vector2.up;
    protected virtual Vector2 GetTargetDir() => Vector2.zero;

    // 저번에 계륵이었던 함수에서 지금은 바꿀 일이 거의 없어진 방식 ((Owner가 있기에 MoveDir을 쉽게 가져올 수 있음.
    protected virtual Vector2 GetAttackDir()
    {
        switch(baseData.directionType)
        {
            case WeaponDirectionType.None:
                return Vector2.zero;
            case WeaponDirectionType.MoveDirection:
                return owner.MoveDirection;
            case WeaponDirectionType.FixedDirection:
                return GetFixedDir();
            case WeaponDirectionType.TargetDirection:
                return GetTargetDir();
        }
        return Vector2.zero;
    }

    // 아까 오너가 Transform을 반환하기 때문에 인자값이 클래스가 아니여도 자식으로 쉽게 들어갈 수 있음.
    public virtual void Initialize(IWeaponOwner owner)
    {
        this.owner = owner;
        transform.SetParent(owner.transform);
        transform.localPosition = Vector3.zero;
    }
    // 쿨타임 방식 개선
    public virtual void Tick(float deltaTime)
    {
        if(coolTimer < baseData.coolTime)
            coolTimer += deltaTime;
            
        if (coolTimer >= baseData.coolTime)
        {
            if (!TryAttack()) return;
            coolTimer = 0f;
        }
    }
    public void Upgrade()
    {
        level++;
    }
}
