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
        fire.SetDirection(GetAttackDir());
    }
    IEnumerator Coroutine_Flame()
    {
        for(int i = 0; i < fireSpawnCount; i++)
        {
            SpawnFire();
            yield return new WaitForSeconds(fireSpawnTerm);
        }
    }

    protected override bool TryAttack()
    {
        StartCoroutine(Coroutine_Flame());
        return true;
    }
}
