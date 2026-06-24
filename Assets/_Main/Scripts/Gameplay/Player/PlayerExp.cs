using System;
using UnityEngine;

public class PlayerExp :
MonoBehaviour, IExpReceiver
{
    [SerializeField] private int maxExp = 100;
    [SerializeField] private int maxLevel = 100;
    private int currentExp = 0;
    private int currentLevel = 0;

    public event Action<int> ExpChanged;
    public event Action<int> LevelChanged;

    public void TakeExp(int amount)
    {
        // Debug.Log($"[PlayerExp] 경험치 획득. 현재 경험치: {currentExp}");
        currentExp += amount;
        if (CanLevelUp())
        {
            LevelUp();
        }
        ExpChanged?.Invoke(currentExp);
    }

    private bool CanLevelUp()
    {
        if (currentExp >= maxExp && maxLevel < currentLevel)
        {
            return true;
        }
        return false;
    }

    private void LevelUp()
    {
        currentLevel += 1;
        currentExp -= maxExp;
        Debug.Log($"[PlayerExp] 레벨업. 현재 레벨: {currentLevel}");

        LevelChanged?.Invoke(currentLevel);
    }
}