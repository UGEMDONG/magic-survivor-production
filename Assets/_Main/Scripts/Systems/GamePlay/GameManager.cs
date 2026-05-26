using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameModeData selectedGameMode;
    [SerializeField] private PlayerClassData selectedPlayerClass;
    
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private TimeFlowSystem timeFlowSystem;

    public GameModeData SelectedGameMode => selectedGameMode;
    public PlayerClassData playerClassData => selectedPlayerClass;


    public void StartGame()
    {
        spawnManager.SpawnPlayer(selectedPlayerClass);
        spawnManager.StartEnemySpawn();

        timeFlowSystem.StartTimer(selectedGameMode);

    }

    public void StartBossStage()
    {
        spawnManager.StopEnemySpawn();
        spawnManager.SpawnBoss();

        timeFlowSystem.StopTimer();
    }
}
