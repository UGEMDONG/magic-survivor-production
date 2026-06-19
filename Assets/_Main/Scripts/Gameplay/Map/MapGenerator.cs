using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private int TypeOfMap = 0;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float size = 1f;
    private GameObject[,] tiles = new GameObject[3,3];
    private Vector2Int currentChunk;

    Vector2Int GetPlayerChunk()
    {
        return new Vector2Int(Mathf.FloorToInt(playerTransform.position.x/size), Mathf.FloorToInt(playerTransform.position.y/size));
    }

    void RearrangeTiles(Vector2Int newChunk)
    {
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                Tile tile = tiles[x,y].GetComponent<Tile>();

                int dx = tile.ChunkCoord.x - newChunk.x;
                int dy = tile.ChunkCoord.y - newChunk.y;

                if(dx < -1)
                    tile.ChunkCoord += Vector2Int.right * 3;
                else if(dx > 1)
                    tile.ChunkCoord += Vector2Int.left * 3;

                if(dy < -1)
                    tile.ChunkCoord += Vector2Int.up * 3;
                else if(dy > 1)
                    tile.ChunkCoord += Vector2Int.down * 3;

                tile.transform.position =new Vector3(tile.ChunkCoord.x * size, tile.ChunkCoord.y * size,0);
            }
        }
    }

    void Awake()
    {
        currentChunk = GetPlayerChunk();
        for (int x = -1; x<=1; x++)
        {
            for (int y = -1; y<=1; y++)
            {
                GameObject tile = Instantiate(mapPrefabs[TypeOfMap]);
                
                tile.GetComponent<Tile>().ChunkCoord = currentChunk + new Vector2Int(x, y);
                tile.transform.position = new Vector3(x*size, y*size, 0);
                tiles[x+1,y+1] = tile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newChunk = GetPlayerChunk();

        if(newChunk != currentChunk)
        {
            RearrangeTiles(newChunk);
            currentChunk = newChunk;
        }
    }
}
