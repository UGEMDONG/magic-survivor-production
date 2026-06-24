using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyHealth[] enemyPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemyParent;

    [Header("Spawn Area")]
    [SerializeField, Min(0f)] private float minimumSpawnDistance = 10f;
    [SerializeField, Min(0f)] private float maximumSpawnDistance = 14f;

    [Header("Difficulty")]
    [SerializeField, Min(0.01f)] private float initialSpawnInterval = 3f;
    [SerializeField, Min(0.01f)] private float minimumSpawnInterval = 0.5f;
    [SerializeField, Min(0f)] private float intervalDecreasePerMinute = 0.5f;
    [SerializeField, Min(1)] private int initialEnemiesPerWave = 1;
    [SerializeField, Min(0.01f)] private float enemyCountIncreaseInterval = 30f;
    [SerializeField, Min(1)] private int maximumEnemiesPerWave = 10;
    [SerializeField, Min(1)] private int maximumActiveEnemies = 100;

    private readonly List<EnemyHealth> spawnedEnemies = new();

    private float elapsedTime;
    private float spawnTimer;
    private bool isSpawning = true;

    public float ElapsedTime => elapsedTime;

    private void Awake()
    {
        if (enemyParent == null)
            enemyParent = transform;
    }

    private void Update()
    {
        if (!isSpawning ||
            player == null ||
            enemyPrefabs == null ||
            enemyPrefabs.Length == 0)
            return;

        elapsedTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        float spawnInterval = GetCurrentSpawnInterval();

        if (spawnTimer < spawnInterval)
            return;

        spawnTimer -= spawnInterval;
        SpawnWave();
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void ResetSpawner()
    {
        elapsedTime = 0f;
        spawnTimer = 0f;
        isSpawning = true;

        foreach (EnemyHealth enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                enemy.gameObject.SetActive(false);
                Destroy(enemy.gameObject);
            }
        }

        spawnedEnemies.Clear();
    }

    private void SpawnWave()
    {
        RemoveDestroyedEnemies();

        int availableCount =
            maximumActiveEnemies - GetActiveEnemyCount();

        int spawnCount = Mathf.Min(
            GetCurrentEnemiesPerWave(),
            availableCount
        );

        for (int i = 0; i < spawnCount; i++)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        EnemyHealth prefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        if (prefab == null)
            return;

        Vector2 direction = Random.insideUnitCircle.normalized;
        float distance = Random.Range(
            minimumSpawnDistance,
            maximumSpawnDistance
        );
        Vector3 spawnPosition =
            player.position + (Vector3)(direction * distance);

        EnemyHealth enemy = Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity,
            enemyParent
        );

        EnemyTargetTracker[] trackers =
            enemy.GetComponentsInChildren<EnemyTargetTracker>(true);

        foreach (EnemyTargetTracker tracker in trackers)
            tracker.SetTarget(player);

        spawnedEnemies.Add(enemy);
    }

    private float GetCurrentSpawnInterval()
    {
        float elapsedMinutes = elapsedTime / 60f;

        return Mathf.Max(
            minimumSpawnInterval,
            initialSpawnInterval -
            elapsedMinutes * intervalDecreasePerMinute
        );
    }

    private int GetCurrentEnemiesPerWave()
    {
        int additionalEnemies =
            Mathf.FloorToInt(elapsedTime / enemyCountIncreaseInterval);

        return Mathf.Min(
            maximumEnemiesPerWave,
            initialEnemiesPerWave + additionalEnemies
        );
    }

    private int GetActiveEnemyCount()
    {
        int activeCount = 0;

        foreach (EnemyHealth enemy in spawnedEnemies)
        {
            if (enemy != null && enemy.gameObject.activeInHierarchy)
                activeCount++;
        }

        return activeCount;
    }

    private void RemoveDestroyedEnemies()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
                spawnedEnemies.RemoveAt(i);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        maximumSpawnDistance = Mathf.Max(
            minimumSpawnDistance,
            maximumSpawnDistance
        );
        minimumSpawnInterval = Mathf.Min(
            initialSpawnInterval,
            minimumSpawnInterval
        );
        maximumEnemiesPerWave = Mathf.Max(
            initialEnemiesPerWave,
            maximumEnemiesPerWave
        );
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center =
            player == null ? transform.position : player.position;

        Gizmos.DrawWireSphere(center, minimumSpawnDistance);
        Gizmos.DrawWireSphere(center, maximumSpawnDistance);
    }
#endif
}
