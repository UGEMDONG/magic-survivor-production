using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public InputActionReference moveAction; 
    private Rigidbody2D rb;
    private Vector2 KeepVector; // 대시 방향을 저장할 변

    // 스크립트가 켜질 때 입력을 활성화합니다.
    private void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    // 스크립트가 꺼질 때 입력을 비활성화합니다. (메모리 낭비 방지)
    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
    }

    void FixedUpdate()
    {
        Vector2 inputDirection = moveAction.action.ReadValue<Vector2>();
        // 2D 게임 기준 (X, Y 축 이동)
        Vector2 moveVector = new Vector2(inputDirection.x, inputDirection.y); 
        rb.MovePosition(rb.position + moveVector * moveSpeed * Time.deltaTime);
    }
}