using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 기능 구현이 내 담당은 아니지만 테스트 겸, 누군가 제시한 컴포넌트 구조화를 고려해서 짠 스크립트
// 공격 방향을 PlayerMove컴포넌트를 통해 가져오는데 그게 원래 없거나 갑자기 없어져도 에러는 안난다(단순히 ㅄ같이 작동할 뿐..)
public class PlayerWeapon : WeaponHandler, IWeaponOwner
{
    SimplePlayerMove mover;
    public Vector3 Position => transform.position;
    private SimplePlayerMove Mover { get
        {
            if (mover == null) mover = GetComponent<SimplePlayerMove>();
            return mover;
        } }

    public Vector3 MoveDirection => !Mover ? Vector3.zero : mover.LastMoveDir;

    // 이거 어떻게 호출하는지 잘 모르겠다면 -> TestPlayer의 PlayerInput 컴포넌트 Player콜백 등록 쪽에 연결되어있어
    public void UpgradeWeapons_Test(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("전체 무기 강화!");
        foreach (IWeapon weapon in GetWeaponList)
            weapon.Upgrade();
    }

    protected override void Start()
    {
        base.Start();

        Initialize(this);
        
        // AddWeapon("Flame");
        // AddWeapon("Lightning");

        AddWeapon("MagicBolt");
        AddWeapon("LavaZone");
    }
    protected override void Update()
    {
        base.Update();
    }
}
