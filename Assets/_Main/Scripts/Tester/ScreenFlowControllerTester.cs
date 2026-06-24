using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenFlowControllerTeset :
MonoBehaviour
{
    [SerializeField] private ScreenFlowController screenFlowController;

    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(ScreenState.Main);
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(ScreenState.MapSelect);
        }
        else if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(ScreenState.Gameplay);
        }
    }
}