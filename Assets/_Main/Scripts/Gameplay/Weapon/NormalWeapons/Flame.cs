using System.Collections;
using UnityEngine;

public class Flame : Weapon
{
    [SerializeField] Fire firePrefab;
    [SerializeField] float fireSpawnTerm = 0.25f;
    [SerializeField] int fireSpawnCount = 5;

    private void SpawnFire()
    {
        Fire fire = Instantiate(firePrefab, transform);
        fire.Initialize(this);
        fire.SetDirection(GetAimDir());
    }
    IEnumerator Coroutine_Flame()
    {
        for(int i = 0; i < fireSpawnCount; i++)
        {
            SpawnFire();
            yield return new WaitForSeconds(fireSpawnTerm);
        }
    }

    // 무기의 virtual로 만든 레벨 업 메서드 활용하기!
    protected override void OnLv2()
    {
        base.OnLv2();
        fireSpawnCount += 2;
    }
    protected override void OnLv3()
    {
        base.OnLv3();
        fireSpawnCount += 3;
    }

    protected override bool TryAttack()
    {
        StartCoroutine(Coroutine_Flame());
        return true;
    }
}
