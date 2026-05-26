using UnityEngine;
using UnityEngine.InputSystem;

// 무기 방향테스트와 움직이는 기능을 위해 만든 간단 컴포넌트
// 여기서 하나만 보자면 뱀서라이크들의 공격 방식을 참고해서, 움직일 땐 그 방향대로 공격 방향을 갖고, 멈출 땐 마지막 공격 방향을 저장한다!
public class SimplePlayerMove : MonoBehaviour
{
    Vector3 moveDir;
    Vector3 lastMoveDir = Vector3.up;   // 첨에 아예 안움직일 수도 있으니까 꼭 초기화

    [SerializeField] float moveSpeed = 3f;

    public Vector3 LastMoveDir => lastMoveDir;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputDir = context.ReadValue<Vector2>();
        if (inputDir == Vector2.zero)
        {
            lastMoveDir = moveDir;
            moveDir = inputDir;
        }
        else moveDir = lastMoveDir = context.ReadValue<Vector2>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * Time.deltaTime * moveSpeed;
    }
}
