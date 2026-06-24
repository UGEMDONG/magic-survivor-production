using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayFlowControllerTester :
MonoBehaviour
{
    [SerializeField] private GameplayFlowController screenFlowController;

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(GameplayState.Playing);
        }
        else if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(GameplayState.Paused);
        }
        else if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            screenFlowController.ChangeState(GameplayState.SkillSelecting);
        }
    }
}