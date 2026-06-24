using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private int TypeOfMap = 0;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float size = 1f;
    private GameObject[,] tiles = new GameObject[3,3];
    private Vector2Int currentChunk;

    public int MapCount => mapPrefabs == null ? 0 : mapPrefabs.Length;

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

    public void RegenerateMap()
    {
        if (MapCount == 0)
        {
            Debug.LogError("[MapGenerator] 생성할 맵 프리팹이 없습니다.");
            return;
        }

        if (TypeOfMap < 0 || TypeOfMap >= MapCount)
        {
            Debug.LogError($"[MapGenerator] 잘못된 맵 인덱스입니다: {TypeOfMap}");
            return;
        }

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] != null)
                    Destroy(tiles[x, y]);
            }
        }

        tiles = new GameObject[3, 3];
        currentChunk = GetPlayerChunk();

        for (int x = -1; x<=1; x++)
        {
            for (int y = -1; y<=1; y++)
            {
                GameObject tile = Instantiate(mapPrefabs[TypeOfMap], transform);

                Vector2Int chunkCoord =
                    currentChunk + new Vector2Int(x, y);

                tile.GetComponent<Tile>().ChunkCoord = chunkCoord;
                tile.transform.position = new Vector3(
                    chunkCoord.x * size,
                    chunkCoord.y * size,
                    0f
                );
                tiles[x+1,y+1] = tile;
            }
        }
    }

    public bool SelectMap(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= MapCount)
        {
            Debug.LogError($"[MapGenerator] 선택할 수 없는 맵 인덱스입니다: {mapIndex}");
            return false;
        }

        TypeOfMap = mapIndex;
        RegenerateMap();
        return true;
    }

    void Awake()
    {
        RegenerateMap();
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
