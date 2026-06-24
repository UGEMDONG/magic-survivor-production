using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public float hpMax = 100;
    public float hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0f)
        {
            hp = 0f;
            this.gameObject.SetActive(false);
        }
    }
}
