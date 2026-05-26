using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float hpMax = 100;
    [SerializeField] float hp;
    [SerializeField] bool isDie;
    [Space]
    [SerializeField] SimpleHpBar hpBar;

    public float HP => hp;

    public bool IsDie => isDie;

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
