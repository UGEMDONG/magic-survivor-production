using UnityEngine;

public class SimpleEnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField, Min(0.1f)]
    private float patrolRadius = 2f;

    [SerializeField, Min(0.01f)]
    private float arriveDistance = 0.15f;

    [Header("Movement")]
    [SerializeField, Min(0f)]
    private float moveSpeed = 0.8f;

    [SerializeField, Min(0f)]
    private float acceleration = 3f;

    [Header("Wait")]
    [SerializeField, Min(0f)]
    private float minWaitTime = 0.4f;

    [SerializeField, Min(0f)]
    private float maxWaitTime = 1.2f;

    private Vector2 patrolCenter;
    private Vector2 targetPosition;
    private Vector2 currentVelocity;

    private float waitTimer;
    private bool isWaiting;

    public Vector2 MoveDirection
    {
        get
        {
            if (currentVelocity.sqrMagnitude <= 0.001f)
                return Vector2.zero;

            return currentVelocity.normalized;
        }
    }

    private void Start()
    {
        patrolCenter = transform.position;
        SelectNextTarget();
    }

    private void Update()
    {
        if (isWaiting)
        {
            UpdateWait();
            return;
        }

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector2 currentPosition = transform.position;
        Vector2 toTarget = targetPosition - currentPosition;

        if (toTarget.sqrMagnitude <= arriveDistance * arriveDistance)
        {
            BeginWait();
            return;
        }

        Vector2 desiredVelocity = toTarget.normalized * moveSpeed;

        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            desiredVelocity,
            acceleration * Time.deltaTime
        );

        transform.position =
            currentPosition + currentVelocity * Time.deltaTime;
    }

    private void UpdateWait()
    {
        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            Vector2.zero,
            acceleration * Time.deltaTime
        );

        if (currentVelocity.sqrMagnitude > 0.001f)
        {
            transform.position +=
                (Vector3)(currentVelocity * Time.deltaTime);
        }

        waitTimer -= Time.deltaTime;

        if (waitTimer > 0f)
            return;

        isWaiting = false;
        SelectNextTarget();
    }

    private void BeginWait()
    {
        isWaiting = true;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    private void SelectNextTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        targetPosition = patrolCenter + randomOffset;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3 center = Application.isPlaying
            ? patrolCenter
            : transform.position;

        Gizmos.DrawWireSphere(center, patrolRadius);

        if (Application.isPlaying)
            Gizmos.DrawSphere(targetPosition, 0.08f);
    }
#endif
}