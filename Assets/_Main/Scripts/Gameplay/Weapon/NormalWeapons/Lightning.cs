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

        // 곱하기 2 해주는 이유: 유니티 기본 원은 스케일 1일 때 반지름 0.5, 그래서 우리 피해가 반지름 2이면 스케일(지름)을 4로 설정해주자
        fx.localScale = State.EffectRadius * Vector3.one * 2f;

        yield return new WaitForSeconds(0.5f);
        Destroy(fx.gameObject);
    }

    protected override void OnLv2()
    {
        base.OnLv2();
        State.StretchEffectRadius(1f);
    }
    protected override void OnLv3()
    {
        base.OnLv3();
        State.StretchEffectRadius(1.5f);
    }

    protected override bool TryAttack()
    {
        // 공격 범위에 있는 적들 하나 가져오기
        var targetsInAttackRadius = Utility.GetNearTargets(transform.position, State.DetectRadius, State.TargetLayers);
        if (targetsInAttackRadius == null || targetsInAttackRadius.Count == 0) return false;

        // 그 적들 중 아무나 하나의 위치 뽑아서 기준으로 공격 넓이에 포함되는 적들 가져오기
        Vector2 randomPos = targetsInAttackRadius[Random.Range(0, targetsInAttackRadius.Count)].transform.position;
        var targetsInAttackArea = Utility.GetNearTargets(randomPos, State.DetectRadius, State.TargetLayers);

        foreach (IDamageable target in targetsInAttackArea)
            target.TakeDamage(State.Damage);
        StartCoroutine(Coroutine_LightningFX(randomPos));

        return true;
    }
}
