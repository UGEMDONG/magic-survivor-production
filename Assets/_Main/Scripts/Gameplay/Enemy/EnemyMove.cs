using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private float Speed = 2f;
    void Start()
    {
        PlayerObject = GameObject.Find("TestPlayer");
    }
    void Update()
    {
        if(Vector2.Distance(PlayerObject.transform.position, transform.position) > 1f)
        {
            Vector3 direction = new Vector3(PlayerObject.transform.position.x - transform.position.x,PlayerObject.transform.position.y - transform.position.y,0f).normalized;
            transform.position += direction * Time.deltaTime * Speed;
        }
    }
}
