using UnityEngine;

public class GameStartFlowController : MonoBehaviour
{
    [SerializeField] private GameObject gameplay;
    [SerializeField] private ScreenFlowController screenFlow;
    [SerializeField] private GameplayFlowController gameplayFlow;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private OrbGen orbGenerator;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerExp playerExp;
    [SerializeField] private Vector3 playerStartPosition;

    private bool isGameStarted;
    private int selectedMapIndex;

    public bool IsGameStarted => isGameStarted;

    private void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerExp = player.GetComponent<PlayerExp>();

        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        playerHealth.Died += HandlePlayerDied;
        playerExp.LevelChanged += HandlePlayerLevelChanged;
    }

    private void OnDisable()
    {
        playerHealth.Died -= HandlePlayerDied;
        playerExp.LevelChanged -= HandlePlayerLevelChanged;
    }

    private void Start()
    {
        ShowMainScreen();
    }

    public void ShowMapSelectScreen()
    {
        gameplay.SetActive(false);

        screenFlow.ChangeState(ScreenState.MapSelect);
    }

    public void SelectMapAndStartGame(int mapIndex)
    {
        gameplay.SetActive(true);

        player.position = playerStartPosition;

        orbGenerator.RegenerateOrbs();
        if (enemySpawner != null)
            enemySpawner.ResetSpawner();

        mapGenerator.SelectMap(mapIndex);

        selectedMapIndex = mapIndex;
        playerHealth.Restart();
        isGameStarted = true;

        gameplayFlow.ChangeState(GameplayState.Playing);
        screenFlow.ChangeState(ScreenState.Gameplay);



        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        gameplay.SetActive(true);
        player.position = playerStartPosition;
        playerHealth.Restart();
        orbGenerator.RegenerateOrbs();
        if (enemySpawner != null)
            enemySpawner.ResetSpawner();

        mapGenerator.SelectMap(selectedMapIndex);

        isGameStarted = true;
        gameplayFlow.ChangeState(GameplayState.Playing);
        screenFlow.ChangeState(ScreenState.Gameplay);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (!isGameStarted || gameplayFlow.CurrentState != GameplayState.Playing)
            return;

        gameplayFlow.ChangeState(GameplayState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (!isGameStarted || gameplayFlow.CurrentState != GameplayState.Paused)
            return;

        gameplayFlow.ChangeState(GameplayState.Playing);
        Time.timeScale = 1f;
    }

    public void CompleteSkillSelection()
    {
        if (!isGameStarted ||
            gameplayFlow.CurrentState != GameplayState.SkillSelecting)
            return;

        gameplayFlow.ChangeState(GameplayState.Playing);
        Time.timeScale = 1f;
    }

    public void ExitToMain()
    {
        ShowMainScreen();
    }

    public void ShowMainScreen()
    {
        gameplay.SetActive(false);

        isGameStarted = false;
        Time.timeScale = 0f;

        gameplayFlow.ChangeState(GameplayState.None);
        screenFlow.ChangeState(ScreenState.Main);
    }

    public void ReturnToMapSelectScreen()
    {
        gameplay.SetActive(false);

        isGameStarted = false;
        Time.timeScale = 0f;

        gameplayFlow.ChangeState(GameplayState.None);
        screenFlow.ChangeState(ScreenState.MapSelect);
    }

    private void HandlePlayerDied()
    {
        if (!isGameStarted)
            return;

        isGameStarted = false;
        gameplayFlow.ChangeState(GameplayState.GameOver);
        Time.timeScale = 0f;
    }

    private void HandlePlayerLevelChanged(int level)
    {
        if (!isGameStarted ||
            gameplayFlow.CurrentState != GameplayState.Playing)
            return;

        gameplayFlow.ChangeState(GameplayState.SkillSelecting);
        Time.timeScale = 0f;
    }
}
