using UnityEngine;

public class ExpOrb : MonoBehaviour
{


    [Header("Orb Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attractionRange = 5f;
    [SerializeField] private Color[] colors = { Color.blue, Color.yellow, Color.magenta };
    public int[] expAmount = { 10, 20, 30 }; 
    public int orbLevel = 1; // 1: Small, 2: Medium, 3: Large
    private Player player;
    private SpriteRenderer sr;

    void MoveToTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void GenerateOrb(Player player, Vector3 position, int level)
    {
        this.player = player;
        orbLevel = level;
        transform.position = position;
        gameObject.SetActive(true);
        sr.color = colors[orbLevel - 1];
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Vector2.Distance(transform.position, player.transform.position) < attractionRange)
        {
            MoveToTarget(player.transform);
            if (Vector3.Distance(transform.position, player.transform.position) < 0.5f)
            {
                Debug.Log("Player collected an EXP orb!");
                StaticGenericPool<ExpOrb>.ReturnPool(this);
                player.GiveExp(expAmount[orbLevel - 1]);
            }
        }
    }
}
