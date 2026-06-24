using System.Collections.Generic;
using UnityEngine;

public class TickDamage : MonoBehaviour
{
    IAttackZone attackZone;

    [SerializeField, Min(0.01f)]
    private float tickInterval = 0.5f;
    private float tickTimer = 0.0f;

    private readonly List<IDamageable> targetBuffer = new();

    private bool IsValidTarget(IDamageable target)
    {
        if (target == null)
            return false;

        // 파괴된 MonoBehaviour인지 검사
        if (target is Object unityObject && unityObject == null)
            return false;

        GameObject targetObject = target.gameObject;

        return targetObject.activeInHierarchy;
    }

    private void ApplyDamage()
    {
        targetBuffer.Clear();

        // 원본 리스트의 현재 상태를 복사
        foreach (IDamageable target in attackZone.Targets)
            targetBuffer.Add(target);

        float damage = attackZone.Weapon.State.Damage;

        foreach (IDamageable target in targetBuffer)
        {
            if (!IsValidTarget(target))
                continue;

            target.TakeDamage(damage);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        attackZone = GetComponent<IAttackZone>();
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer < tickInterval)
            return;

        tickTimer -= tickInterval;
        ApplyDamage();
    }
}
