using System.Collections.Generic;
using UnityEngine;

// 어차피 얘는 씬 안에 하나가 무조건 생성되고 2개는 필요 없는 전형적인 싱글톤 매니저일 것 같아서 바꿈\
// 그리고 이게 상속 구조 상 더 이상 상속 할게 있나 싶어서 싱글톤 반환 형은 인터페이스가 아니라 자기 자신으로 함
public class WeaponProvider : MonoBehaviour, IWeaponProvider
{
    static WeaponProvider instance;

    [SerializeField] private List<GameObject> allWeaponsObject = new List<GameObject>();
    private List<IWeapon> allWeapons = new List<IWeapon>();

    public static WeaponProvider Instance => instance;

    public IReadOnlyList<IWeapon> GetWeaponList => allWeapons;
    public IWeapon NameToWeapon(string weaponName)
    {
        foreach (IWeapon weapon in allWeapons)
        {
            if (weapon.WeaponName == weaponName)
            {
                Debug.Log($"return {weaponName}");
                return weapon;
            }
        }
        Debug.Log($"null {weaponName}");
        return null;
    }

    void Awake()    // 원래 Start였는데 이런 매니저형 초기화는 Awake 추천!!
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        foreach (GameObject weaponObject in allWeaponsObject)
        {   
            // 씬에 게임오브젝트로 인스턴스 안하면 프리펩 훼손됨 클나!!
            var weapon = Instantiate(weaponObject, transform);
            allWeapons.Add(weapon.GetComponent<IWeapon>());
        }
    }
}
