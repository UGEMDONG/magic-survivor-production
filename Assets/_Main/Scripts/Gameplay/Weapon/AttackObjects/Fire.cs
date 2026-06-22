using UnityEngine;

public class Fire : AttackObject
{
    Vector3 direction;
    // [SerializeField] Animation animation;

    public void AnimEvent_OnBurn()
    {
        attackCollider.enabled = true;
    }
    public void AnimEvent_OnEnd()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        transform.up = direction;
    }

    /*protected override void Start()
    {
        base.Start();
        animation.Play();
    }*/
}
