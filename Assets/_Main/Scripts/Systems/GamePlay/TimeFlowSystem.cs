using UnityEngine;

public class TimeFlowSystem : MonoBehaviour
{
    private GameModeData gameModeData;

    public void StartTimer(GameModeData gameModeData)
    {
        this.gameModeData = gameModeData;
    }

    public void StopTimer()
    {
        
    }
}