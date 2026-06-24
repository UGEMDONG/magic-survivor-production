using UnityEngine;

public class EnemyTargetTracker : 
MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform target;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        TrackTarget();
    }

    private void TrackTarget()
    {
        if (target == null || rb == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed * Time.deltaTime;
    }

}
