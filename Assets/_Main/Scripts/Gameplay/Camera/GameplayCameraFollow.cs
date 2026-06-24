using UnityEngine;

public class GameplayCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ScreenFlowController screenFlow;
    [SerializeField] private Vector3 followOffset = new(0f, 0f, -10f);

    private Vector3 homePosition;
    private bool isFollowing;

    private void Awake()
    {
        homePosition = transform.position;
    }

    private void OnEnable()
    {
        if (screenFlow != null)
            screenFlow.ScreenStateChanged += HandleScreenStateChanged;
    }

    private void OnDisable()
    {
        if (screenFlow != null)
            screenFlow.ScreenStateChanged -= HandleScreenStateChanged;
    }

    private void LateUpdate()
    {
        if (!isFollowing || player == null)
            return;

        transform.position = player.position + followOffset;
    }

    private void HandleScreenStateChanged(
        ScreenState previousState,
        ScreenState currentState)
    {
        isFollowing = currentState == ScreenState.Gameplay;

        if (isFollowing)
        {
            if (player != null)
                transform.position = player.position + followOffset;

            return;
        }

        transform.position = homePosition;
    }
}
