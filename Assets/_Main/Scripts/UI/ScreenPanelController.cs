using System.Collections.Generic;
using UnityEngine;

public class ScreenPanelController :
MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject mapSelectPanel;
    [SerializeField] private GameObject gameplayPanel;

    [SerializeField] private ScreenFlowController screenFlow;

    private List<GameObject> panels = new List<GameObject>();

    void Awake()
    {
        panels.Add(mainPanel);
        panels.Add(mapSelectPanel);
        panels.Add(gameplayPanel);
    }

    void OnEnable()
    {
        screenFlow.ScreenStateChanged += OnScreenStateChanged;
    }
    void OnDisable()
    {
        screenFlow.ScreenStateChanged -= OnScreenStateChanged;
    }


    private void OnScreenStateChanged(ScreenState previousState, ScreenState currentState)
    {
        if (currentState == ScreenState.Main)
        {
            ChangePanel(mainPanel);
        }
        else if (currentState == ScreenState.MapSelect)
        {
            ChangePanel(mapSelectPanel);
        }
        else if (currentState == ScreenState.Gameplay)
        {
            ChangePanel(gameplayPanel);
        }
        Debug.Log($"[ScreenPanelController] 화면 전환 시도: {currentState}");
    }

    private void ChangePanel(GameObject nextPanel)
    {
        DisableAllPanel();
        nextPanel.SetActive(true);
        Debug.Log($"[ScreenPanelController] 화면 전환 성공");
    }

    private void DisableAllPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

}