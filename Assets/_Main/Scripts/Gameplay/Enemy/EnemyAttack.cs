using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    private bool isPlayerInContact = false;
    private float damageTimer = 0f;
    public float damageInterval = 1f;
    [SerializeField] private GameObject PlayerObject;

    void Start()
    {
        PlayerObject = GameObject.Find("TestPlayer");
    }
    void Update()
    {
        if (isPlayerInContact)
        {
        damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                PlayerObject.GetComponent<PlayerHp>().TakeDamage(10f);
                damageTimer = 0f; // 타이머 초기화
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = true; // 닿기 시작함
            damageTimer = damageInterval; // 닿자마자 데미지를 주려면 타이머를 꽉 채워둠
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = false; // 떨어짐
        }
    }
}
