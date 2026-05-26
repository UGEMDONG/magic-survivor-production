using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Lightning : Weapon
{
    [SerializeField] GameObject effectPrefab;

    IEnumerator Coroutine_LightningFX(Vector3 position)
    {
        Transform fx = Instantiate(effectPrefab).transform;
        fx.position = position;
        fx.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(fx.gameObject);
    }

    protected override bool TryAttack()
    {
        // 공격 범위에 있는 적들 하나 가져오기
        var targetsInAttackRadius = Utility.GetNearTargets(transform.position, baseData.attackRadius, baseData.targetLayers);
        if (targetsInAttackRadius == null || targetsInAttackRadius.Count == 0) return false;

        // 그 적들 중 아무나 하나의 위치 뽑아서 기준으로 공격 넓이에 포함되는 적들 가져오기
        Vector2 randomPos = targetsInAttackRadius[Random.Range(0, targetsInAttackRadius.Count)].transform.position;
        var targetsInAttackArea = Utility.GetNearTargets(randomPos, baseData.attackRange, baseData.targetLayers);

        foreach (IDamageable target in targetsInAttackArea)
            target.TakeDamage(baseData.damage);
        StartCoroutine(Coroutine_LightningFX(randomPos));

        return true;
    }
}
