using UnityEngine;

public class GameplayPanelController :
MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject skillSelectingPanel;

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
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
    }
    private void Play()
    {
        pausePanel.SetActive(false);
        skillSelectingPanel.SetActive(false);
    }
    private void SelectSkill()
    {
        skillSelectingPanel.SetActive(true);
    }
}