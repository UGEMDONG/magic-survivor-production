using System.Collections.Generic;
using UnityEngine;

// 프로토타입의 WeaponHolder를 추상으로 만들어서 무기를 쓰는 모든 게임 오브젝트들이 사용하기 위해 확장성이 좋게 만들자는 의견이 나와서
// 개인적으로 컴포넌트화 해서 사용하는 추상 클래스 만들었음. 이름 바꾼 이유: 무기를 받는 것 보단 사용하는 것에 중점.
public abstract class WeaponHandler : MonoBehaviour, IWeaponReceiver
{
    IWeaponOwner owner;
    // [SerializeField] WeaponProvider provider;
    List<IWeapon> weaponList = new();
    private bool initialized;

    public IReadOnlyList<IWeapon> GetWeaponList => weaponList;


    public void Initialize(IWeaponOwner owner)
    {
        this.owner = owner;
        initialized = true;
    }

    // 저번엔 ReceiveWeapon를 딱히 활용 안했었는데 리스트에 추가하는 최종 메서드 용도가 좋을듯
    public void ReceiveWeapon(IWeapon weapon)
    {
        weapon.Initialize(owner);
        weaponList.Add(weapon);
    }
    public void AddWeapon(string weaponName)
    {
        if(!initialized)
        {
            Debug.LogWarning("WeaponHandler가 초기화되지 않았습니다!!");
            return;
        }
        var weapon = WeaponProvider.Instance.NameToWeapon(weaponName);

        if (weapon == null)
        {
            Debug.LogWarning("이름에 맞는 무기가 없습니다!!");
            return;
        }
        ReceiveWeapon(weapon);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float deltaTime = Time.deltaTime;
        foreach (IWeapon weapon in weaponList)
            weapon.Tick(deltaTime);
    }
}
