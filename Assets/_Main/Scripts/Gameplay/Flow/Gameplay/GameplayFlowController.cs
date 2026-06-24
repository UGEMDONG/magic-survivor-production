using UnityEngine;
using System;

public class GameplayFlowController :
MonoBehaviour 
{
    private GameplayState gameplayState;

    public GameplayState CurrentState => gameplayState;

    public event Action<GameplayState, GameplayState> GameplayStateChanged;


    void Awake()
    {
        gameplayState = GameplayState.Playing;
    }

    public void ChangeState(GameplayState gameplayState)
    {
        GameplayState previousState = this.gameplayState;
        this.gameplayState = gameplayState;

        GameplayStateChanged?.Invoke(previousState, this.gameplayState);
    }


}