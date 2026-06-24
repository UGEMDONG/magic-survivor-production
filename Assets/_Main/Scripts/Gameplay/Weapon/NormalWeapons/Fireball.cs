using System.Collections;
using UnityEngine;

public class Fireball : Weapon
{
    [SerializeField] Projectile fireballPrefab;
    [SerializeField] int fireDirCount = 4;
    [SerializeField] int fireCountPerDir = 4;

    IEnumerator Coroutine_FireBalls()
    {
        float splitAngle = Mathf.PI * 2f / fireDirCount;
        for (int i = 0; i < fireCountPerDir; i++)
        {
            for (int j = 0; j < fireDirCount; j++)
            {
                float dirX = Mathf.Cos(j * splitAngle);
                float dirY = Mathf.Sin(j * splitAngle);
                Vector3 dir = new Vector3(dirX, dirY, 0.0f);

                var fireball = Instantiate(fireballPrefab);
                fireball.Initialize(this, 7f);
                fireball.transform.position = transform.position;
                fireball.transform.up = dir;
            }
            yield return new WaitForSeconds(0.28f);
        }      
    }

    protected override bool TryAttack()
    {
        StartCoroutine(Coroutine_FireBalls());
        return true;
    }

    protected override void OnLv2()
    {
        base.OnLv2();
        fireCountPerDir += 2;
    }
    protected override void OnLv3()
    {
        base.OnLv3();
        fireDirCount += 2;
    }
}
