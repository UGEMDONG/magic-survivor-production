using UnityEngine;


[CreateAssetMenu(fileName = "NewGameModeData", menuName = "ScriptableObjects/Game Mode Data")]
public class GameModeData : ScriptableObject
{
    [SerializeField] private string modeName;
    [SerializeField] private bool bossSpawn;
    [SerializeField] private float bossSpawnTime;
    [SerializeField] private bool infinityMode;

    public string ModeName => modeName;
    public bool BossSpawn => bossSpawn;
    public float BossSpawnTime => BossSpawnTime;
    public bool InfinityMode => infinityMode;
}
