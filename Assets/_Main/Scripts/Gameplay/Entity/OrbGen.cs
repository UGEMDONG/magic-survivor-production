using UnityEngine;

public class OrbGen : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private ExpOrb expOrbPrefab;
    [SerializeField] private int pollLimit = 200;
    private float currentCountDown = 0f;
    [SerializeField] private float spawnInterval;
    [System.Serializable]
    public struct SpawnPositions
    {
        public Vector3 left;
        public Vector3 right;
    }

    [SerializeField]
    private SpawnPositions spawnPositions;

    public void GenerateOrbs(Vector3 position, int level)
    {
        ExpOrb orb = StaticGenericPool<ExpOrb>.GetPool(expOrbPrefab, position);

        orb.GenerateOrb(player, position, level);
    }

    void Start()
    {
        StaticGenericPool<ExpOrb>.Preload(expOrbPrefab, pollLimit);
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3(Random.Range(spawnPositions.left.x, spawnPositions.right.x), Random.Range(spawnPositions.left.y, spawnPositions.right.y), Random.Range(spawnPositions.left.z, spawnPositions.right.z));
        return pos;
    }

    void Update()
    {
        currentCountDown += Time.deltaTime;
        if (currentCountDown >= spawnInterval)
        {
            currentCountDown = 0f;
            GenerateOrbs(RandomPosition(), Random.Range(1, 4));
        }
        // if you want dead monsters to drop orbs, call GenerateOrbs() in the monster's death function with the monster's position and desired orb level
    }
}
