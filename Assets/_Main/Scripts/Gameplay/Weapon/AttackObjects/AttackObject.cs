using UnityEngine;

// 무기 데이타 브랜치 이후 달라진 점:
// 1. 유니티 충돌 콜백 자체를 사용하지 않음, 이유는 공격을 넣는 방식, 시점조차 구현하거나 알면은 안된다고 생각.
// 2. 자연스럽게 공격 시에도 사라지는 공격 오브젝트인지 알 수 없다고 판단해서 destroyOnAttack도 없앴다.
// 한마디로 ==> 더 간단해지고 넓어진 개념이 되어따.
public abstract class AttackObject : MonoBehaviour
{
    protected IWeapon weapon;

    [SerializeField] protected Collider2D attackCollider;
    [SerializeField] protected float lifeTime = 1f;

    public IWeapon Weapon => weapon;

    public virtual void Initialize(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    protected virtual void Awake()
    {
        if (attackCollider == null)
            attackCollider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        TickLifeTime();
    }

    protected virtual void TickLifeTime()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
            Destroy(gameObject);
    }
}
