using UnityEngine;
using System;

// 컴포넌트 분리 구조를 생각하다 그럼 충돌 호출도 한 컴포넌트가 담당하고 콜백 호출로 알려줄까 라고 생각했지만
// 거의 모든 컴포넌트가 모노비헤이비어고 계층 구조에 문제가 있는게 아니면 그냥 자신이 구현하는게 나을 듯..(아직 안 쓸 예정)
public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] Collider2D ownCollider;

    public event Action<Collision2D> onCollisionEnter;
    public event Action<Collision2D> onCollisionStay;
    public event Action<Collision2D> onCollisionExit;

    public event Action<Collider2D> onTriggerEnter;
    public event Action<Collider2D> onTriggerStay;
    public event Action<Collider2D> onTriggerExit;

    public void Initialize(bool isTrigger)
    {
        ownCollider.isTrigger = isTrigger;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEnter?.Invoke(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        onCollisionStay?.Invoke(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onCollisionExit?.Invoke(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke(collision);   
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        onTriggerStay?.Invoke(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit?.Invoke(collision);
    }
}
