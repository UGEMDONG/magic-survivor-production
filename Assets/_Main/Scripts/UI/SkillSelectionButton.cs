using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class SkillSelectionButton : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private GameStartFlowController gameFlow;
    [SerializeField] private TMP_Text skillNameText;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (skillNameText == null)
            skillNameText = GetComponentInChildren<TMP_Text>(true);

        button.onClick.AddListener(SelectSkill);
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(SelectSkill);
    }

    public void Configure(
        string newWeaponName,
        PlayerWeapon newPlayerWeapon,
        GameStartFlowController newGameFlow)
    {
        weaponName = newWeaponName;
        playerWeapon = newPlayerWeapon;
        gameFlow = newGameFlow;

        IWeapon ownedWeapon = playerWeapon.FindWeapon(weaponName);

        if (skillNameText == null)
            return;

        skillNameText.text = ownedWeapon == null
            ? $"{weaponName}\nNEW"
            : $"{weaponName}\nLv.{ownedWeapon.Level} → Lv.{ownedWeapon.Level + 1}";
    }

    public void SelectSkill()
    {
        if (playerWeapon == null || gameFlow == null)
        {
            Debug.LogError(
                "[SkillSelectionButton] PlayerWeapon 또는 GameFlow가 연결되지 않았습니다."
            );
            return;
        }

        if (!playerWeapon.AcquireOrUpgradeWeapon(weaponName))
            return;

        gameFlow.CompleteSkillSelection();
    }
}
