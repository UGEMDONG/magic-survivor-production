using System.Collections;
using UnityEngine;

public class Cyclone : Weapon
{
    [SerializeField] AttackZone cyclonePrefab;
    [SerializeField] int cycloneCount = 5;

    IEnumerator Coroutine_Cyclone()
    {
        for (int j = 0; j < cycloneCount; j++)
        {
            var cyclone = Instantiate(cyclonePrefab);
            cyclone.Initialize(this);
            cyclone.transform.position = transform.position;
            cyclone.transform.up = GetAimDir();
        }
        yield return new WaitForSeconds(0.28f);
    }

    protected override bool TryAttack()
    {
        StartCoroutine(Coroutine_Cyclone());
        return true;
    }

    protected override void OnLv2()
    {
        base.OnLv2();
        cycloneCount += 2;
    }
    protected override void OnLv3()
    {
        base.OnLv3();
        cycloneCount += 3;
    }
}
