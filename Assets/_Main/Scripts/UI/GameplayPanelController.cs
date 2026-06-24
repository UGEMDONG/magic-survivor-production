using UnityEngine;

public class GameplayPanelController :
MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject skillSelectingPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameplayFlowController gameplayFlow;

    void OnEnable()
    {
        gameplayFlow.GameplayStateChanged += OnGameplayFlowChanged;
    }
    void OnDisable()
    {
        gameplayFlow.GameplayStateChanged -= OnGameplayFlowChanged;
    }


    private void OnGameplayFlowChanged(GameplayState previousState, GameplayState currentState)
    {
        if (currentState == GameplayState.Paused)
        {
            Debug.Log($"{previousState} => {currentState}");
            if (previousState == GameplayState.Playing)
            {
                Pause();
            }
        }
        else if (currentState == GameplayState.Playing)
        {
            Play();
        }
        else if (currentState == GameplayState.SkillSelecting)
        {
            SelectSkill();
        }
        else if (currentState == GameplayState.GameOver)
        {
            ShowGameOver();
        }
        else if (currentState == GameplayState.None)
        {
            HideAllPanels();
        }
    }

    private void Pause()
    {
        SetPanelActive(pausePanel, true);
    }
    private void Play()
    {
        HideAllPanels();
    }
    private void SelectSkill()
    {
        HideAllPanels();
        SetPanelActive(skillSelectingPanel, true);
    }

    private void ShowGameOver()
    {
        HideAllPanels();
        SetPanelActive(gameOverPanel, true);
    }

    private void HideAllPanels()
    {
        SetPanelActive(pausePanel, false);
        SetPanelActive(skillSelectingPanel, false);
        SetPanelActive(gameOverPanel, false);
    }

    private static void SetPanelActive(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }
}
