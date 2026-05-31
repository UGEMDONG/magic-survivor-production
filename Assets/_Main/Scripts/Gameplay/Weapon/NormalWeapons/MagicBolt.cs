using UnityEngine;

// 프로토타입의 Bow와 비슷한 타겟 방향 공격 예제인데, 이번엔 원작의 "마력탄(Magic Bolt)"를 참고해서 유도 투사체 공격으로 만들었다.

public class MagicBolt : Weapon
{
    IDamageable target;
    [SerializeField] Bolt boltPrefab;

    protected override Vector2 GetTargetDir()
    {
        if (target != null) return Utility.GetNormalizedDir(target.transform.position
            , transform.position);  // Owner의 위치가 더 표현이 정확할 순 있는데 울 게임 모든 무기의 위치는 로컬 원점인걸로
        return Vector2.zero;
    }

    protected override bool TryAttack()
    {
        // 위 아래 방식 두개 다 좋은데 GetTargetDir예시로 쓰기 위해서 전자로 ㄱㄱ
        if (GetAttackDir() == Vector2.zero)
            return false;
        // if (target == null) return false; 

        Bolt bolt = Instantiate(boltPrefab);
        bolt.transform.position = transform.position;
        bolt.Initialize(this, target);

        return true;
    }

    public override void Tick(float deltaTime)
    {
        // 찾는게 먼저, 그리고 공격
        target = Utility.GetNearestTarget2D(transform.position, State.DetectRadius, State.TargetLayers);      
        base.Tick(deltaTime);
    }
}
