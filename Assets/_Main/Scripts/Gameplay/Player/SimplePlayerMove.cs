using UnityEngine;
using UnityEngine.InputSystem;

// 무기 방향테스트와 움직이는 기능을 위해 만든 간단 컴포넌트
// 여기서 하나만 보자면 뱀서라이크들의 공격 방식을 참고해서, 움직일 땐 그 방향대로 공격 방향을 갖고, 멈출 땐 마지막 공격 방향을 저장한다!
public class SimplePlayerMove : MonoBehaviour
{
    Vector3 lastMoveDir = Vector3.up;   // 첨에 아예 안움직일 수도 있으니까 꼭 초기화

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] private float directionChangeDelay = 0.08f;

    private Vector2 moveInput;
    private Vector2 pendingDirection;
    private float pendingTimer;

    public Vector3 LastMoveDir =>
        new Vector3(lastMoveDir.x, lastMoveDir.y, 0f);

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = input.normalized;

        if (input.sqrMagnitude <= 0.001f)
        {
            pendingTimer = 0f;
            return;
        }

        Vector2 normalized = input.normalized;

        bool previousWasDiagonal =
            Mathf.Abs(lastMoveDir.x) > 0.01f &&
            Mathf.Abs(lastMoveDir.y) > 0.01f;

        bool currentIsCardinal =
            Mathf.Abs(normalized.x) <= 0.01f ||
            Mathf.Abs(normalized.y) <= 0.01f;

        // 대각선에서 한 키만 먼저 떨어진 상황일 수 있으므로 잠시 보류
        if (previousWasDiagonal && currentIsCardinal)
        {
            if (pendingDirection != normalized)
            {
                pendingDirection = normalized;
                pendingTimer = 0f;
            }

            return;
        }

        lastMoveDir = normalized;
        pendingTimer = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveInput * Time.deltaTime * moveSpeed;

        if (moveInput.sqrMagnitude <= 0.001f)
            return;

        if (pendingDirection.sqrMagnitude <= 0.001f)
            return;

        pendingTimer += Time.deltaTime;

        if (pendingTimer >= directionChangeDelay)
        {
            lastMoveDir = pendingDirection;
            pendingDirection = Vector2.zero;
            pendingTimer = 0f;
        }
    }
}
