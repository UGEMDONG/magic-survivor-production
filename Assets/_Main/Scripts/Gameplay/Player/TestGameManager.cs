using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestGameManager : MonoBehaviour
{
    public GameObject PlayerObject;
    PlayerHp player;
    public Slider HPSlider;
    public TextMeshProUGUI hpText;

    void Start()
    {
        PlayerObject = GameObject.Find("TestPlayer");
        player = PlayerObject.GetComponent<PlayerHp>();
    }
    void Update()
    {
        if (player != null)
        {
            HPSlider.value = player.hp / player.hpMax;
            hpText.text = $"HP: {player.hp}/{player.hpMax}";
        }
    }
}
