using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, ITargetable
{
    [SerializeField] float hpMax = 100;
    [SerializeField] float hp;
    [SerializeField] bool isDie;
    [Space]
    [SerializeField] SimpleHpBar hpBar;
    [Space]
    [SerializeField, Min(0f)] float contactDamagePerSecond = 10f;

    public float HP => hp;

    public bool IsDie => isDie;

    public bool IsTargetable => true;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            hp = 0f;
            isDie = true;
            Invoke("Rebirth", 2f);
            gameObject.SetActive(false);
        }
        hpBar.SetValue(hp / hpMax);
    }

    private void Rebirth()
    {
        TakeDamage(-hpMax);
        isDie = false;
        gameObject.SetActive(true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ApplyContactDamage(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ApplyContactDamage(other);
    }

    private void ApplyContactDamage(Collider2D other)
    {
        if (isDie || contactDamagePerSecond <= 0f)
            return;

        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null || playerHealth.IsDie)
            return;

        playerHealth.TakeDamage(contactDamagePerSecond * Time.fixedDeltaTime);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
