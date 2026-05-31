using UnityEngine;

public abstract class AttackObject : MonoBehaviour
{
    // 이거도 프로토타입과 달라진게 거의 이거밖에 없음 -> 오너 추가, 무기의 최종 주인이 아닌 자신을 사용하는 무기를 오너로 정하자
    // 이유: 필요한 모든 데이터를 무기에게서 얻을 수 있을 것 같고, 나중에 무기를 통해 오너를 알 수도 있으니까.
    protected IWeapon owner;

    [SerializeField] protected float damage;
    [SerializeField] protected Vector3 direction;   // 포지션에 바로 더해진 방향 벡터는 Vector3로 하자!
    [SerializeField] protected float speed = 5f;
    [Space]
    [SerializeField] protected Collider2D attackCollider;
    [SerializeField] protected float lifeTime = 1f;
    [Space]
    [SerializeField] protected bool destroyOnAttack = false;

    public virtual void Initialize(IWeapon owner)
    {
        this.owner = owner;
        damage = owner.State.Damage;
    }

    protected virtual void Start()
    {
        if (attackCollider == null)
            attackCollider = GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider == null) return;
        IDamageable hitDamageableObject = hitCollider.GetComponent<IDamageable>();

        if (hitDamageableObject == null) return;
        hitDamageableObject.TakeDamage(damage);
        if (destroyOnAttack) Destroy(gameObject);
    }

    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
