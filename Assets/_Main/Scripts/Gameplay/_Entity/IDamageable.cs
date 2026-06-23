using UnityEngine;

public interface IDamageable
{
    public Transform transform { get; }
    // 원래 게임오브젝트까진 필요 없었는데 activeSelf 체크때문에 이젠 필요해져버리네;
    public GameObject gameObject { get; }
    public float HP { get; }
    public bool IsDie { get; }
    public void TakeDamage(float damage);
}
