using System.Collections.Generic;
using UnityEngine;

// 정규화 방향 벡터와 주변 타겟들, 그 중 가장 가까운 타겟 하나 찾기 같이
// 자주 쓰이고 범용적인 기능을 모아놓은 정적 클래스
public static class Utility
{
    public static Vector3 GetNormalizedDir(Vector3 to, Vector3 from)
        => (to - from).normalized;

    public static List<ITargetable> GetNearTargets(Vector2 position, float attackRadius, LayerMask targetLayers)
    {
        // 가상의 원 물리 판정으로 콜라이더들 가져오기
        var cols = Physics2D.OverlapCircleAll(position, attackRadius, targetLayers);
        if (cols == null || cols.Length == 0) return null;
        // 콜라이더마다 IDamageable이 붙어있으면 저장
        List<ITargetable> targets = new();
        foreach (var col in cols)
            if (col.TryGetComponent<ITargetable>(out ITargetable target))
                targets.Add(target);
        // 하나도 없으면 null 리턴
        if (targets.Count == 0) return null;
        return targets;
    }
    public static ITargetable GetNearestTarget2D(Vector2 position, float attackRadius, LayerMask targetLayers)
    {
        var targets = GetNearTargets(position, attackRadius, targetLayers);
        if (targets == null || targets.Count == 0) return null;

        // 일반적인 최소/최대 찾기 알고리즘
        int resultIndex = -1;
        int minDist = int.MaxValue;

        for(int i = 0; i < targets.Count; i++)
        {
            float dist = Vector2.Distance(position, targets[i].transform.position);
            if (dist < minDist) resultIndex = i;
        }
        return targets[resultIndex];
    }
}
