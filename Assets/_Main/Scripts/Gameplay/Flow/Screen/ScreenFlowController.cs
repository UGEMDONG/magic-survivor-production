using UnityEngine;
using System;

public class ScreenFlowController :
MonoBehaviour
{
    private ScreenState screenState;

    public event Action<ScreenState, ScreenState> ScreenStateChanged;

    void Awake()
    {
        screenState = ScreenState.None;
        ChangeState(ScreenState.Main);
    }

    public void ChangeState(ScreenState screenState)
    {
        ScreenState previousState = this.screenState;
        this.screenState = screenState;

        ScreenStateChanged?.Invoke(previousState, this.screenState);
    } 
}