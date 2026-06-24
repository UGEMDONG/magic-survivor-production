using System;
using UnityEngine;

public class PlayerHealth :
MonoBehaviour, IDamageable
{
    [SerializeField] float hpMax = 100;
    [SerializeField] float hp;
    [SerializeField] bool isDie;
    [Space]
    [SerializeField] SimpleHpBar hpBar;

    public event Action Died;

    public float HP => hp;

    public bool IsDie => isDie;

    public bool IsTargetable => true;

    public void TakeDamage(float damage)
    {
        if (isDie)
            return;

        hp -= damage;
        if(hp <= 0f)
        {
            hp = 0f;
            isDie = true;
            Died?.Invoke();
        }
        hpBar.SetValue(hp / hpMax);
    }

    public void Restart()
    {
        hp = hpMax;
        isDie = false;
        hpBar.SetValue(1f);
    }

    void Awake()
    {
        Restart();
    }


}
