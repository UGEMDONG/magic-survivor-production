using System.Collections.Generic;
using UnityEngine;

public class GameplayPanelController :
MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject skillSelectingPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameplayFlowController gameplayFlow;
    [SerializeField] private GameStartFlowController gameFlow;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private SkillSelectionButton[] skillButtons;

    private void Awake()
    {
        if ((skillButtons == null || skillButtons.Length == 0) &&
            skillSelectingPanel != null)
        {
            skillButtons =
                skillSelectingPanel.GetComponentsInChildren
                    <SkillSelectionButton>(true);
        }
    }

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

        if (RefreshSkillChoices())
            SetPanelActive(skillSelectingPanel, true);
    }

    private bool RefreshSkillChoices()
    {
        List<string> candidates = GetSkillCandidates();
        Shuffle(candidates);

        if (skillButtons == null)
            skillButtons = new SkillSelectionButton[0];

        if (skillButtons.Length == 0)
        {
            Debug.LogError(
                "[GameplayPanelController] 스킬 선택 버튼이 없습니다."
            );

            if (gameFlow != null)
                gameFlow.CompleteSkillSelection();

            return false;
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i] == null)
                continue;

            bool hasSkill = i < candidates.Count;
            skillButtons[i].gameObject.SetActive(hasSkill);

            if (hasSkill)
            {
                skillButtons[i].Configure(
                    candidates[i],
                    playerWeapon,
                    gameFlow
                );
            }
        }

        if (candidates.Count == 0)
        {
            Debug.Log("선택 가능한 스킬이 없습니다.");

            if (gameFlow != null)
                gameFlow.CompleteSkillSelection();

            return false;
        }

        return true;
    }

    private List<string> GetSkillCandidates()
    {
        List<string> candidates = new();
        HashSet<string> addedNames = new();

        if (WeaponProvider.Instance == null || playerWeapon == null)
            return candidates;

        foreach (IWeapon weapon in WeaponProvider.Instance.GetWeaponList)
        {
            if (weapon == null || !addedNames.Add(weapon.WeaponName))
                continue;

            IWeapon ownedWeapon = playerWeapon.FindWeapon(weapon.WeaponName);

            if (ownedWeapon != null &&
                ownedWeapon.Level >= ownedWeapon.MaxLevel)
                continue;

            candidates.Add(weapon.WeaponName);
        }

        return candidates;
    }

    private static void Shuffle(List<string> skills)
    {
        for (int i = skills.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (skills[i], skills[randomIndex]) =
                (skills[randomIndex], skills[i]);
        }
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
