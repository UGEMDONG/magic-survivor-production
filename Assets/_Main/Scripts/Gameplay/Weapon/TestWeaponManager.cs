using UnityEngine;
using UnityEngine.UI;

public class TestWeaponManager : MonoBehaviour
{
    [SerializeField] Transform addWeaponBtnRoot;
    // [SerializeField] Button btnPrefab;
    [SerializeField] PlayerWeapon weaponPlayer;

    public void AddPlayerWeapon(string weaponName)
    {
        weaponPlayer.AddWeapon(weaponName);
    }
    public void UpgradeWeaponsAll()
    {
        weaponPlayer.UpgradeWeaponsAll();
    }
    public void DeleteWeaponsAll()
    {
        weaponPlayer.DeleteWeaponsAll();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var btns = addWeaponBtnRoot.GetComponentsInChildren<Button>();
        foreach (var btn in btns)
        {
            btn.GetComponentInChildren<Text>().text 
                = "Add Weapon: " + btn.gameObject.name;
        }

        /*var weaponObjs = WeaponProvider.Instance.AllWeaponObjs;
        foreach(var obj in weaponObjs)
        {
            if (!obj.TryGetComponent<IWeapon>(out IWeapon weapon))
                continue;
            var btn = Instantiate(btnPrefab, addWeaponBtnRoot);
            btn.GetComponentInChildren<Text>().text = weapon.WeaponName + " 추가하기";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
