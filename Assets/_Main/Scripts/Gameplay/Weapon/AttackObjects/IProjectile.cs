using UnityEngine;

// 이 인터페이스는 사용 할지 말지 고민중이다. 그 이유는 사용 시 투사체의 사용 가능 범위를 확장하고
// 의존 역전 원칙에 따라 구현을 모르게 하는 철학에 더 가까워질 수는 있지만,
// 엔진에서의 직관적인 흐름에서 파악하기 어렵거나 비효율적이고, 지금 게임 개발 진도와 게임 규모에서
// 필요한지에 대한 의문이 있기 때문에 계속 고민중이다.
// + 그냥 IProjectile보다 투사체 정보를 가졌다 라는 뜻의 IProjectileContext?도 나쁘지 않은듯
public interface IProjectile : IDetectable
{
    public float Speed { get; }
    public Vector3 Direction { get; }
    // public ITargetable Target { get; }

    public void Initialize(IWeapon owner, float speed, Vector3 direction, ITargetable target = null);
}
