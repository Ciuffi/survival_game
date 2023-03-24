using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private Color colorTint;
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private GameObject[] flourishPrefabs;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private int flourishSize = 4;

    [SerializeField] private int gridSize = 5;

    private GameObject playerPrefab;
    private Transform player;
    private Vector2 backgroundSize;
    private Queue<GameObject> backgroundPool;
    private Queue<GameObject> flourishPool;
    public int flourishMin = 1, flourishMax = 3;

    private Dictionary<Vector2Int, GameObject> grid;
    private Dictionary<Vector2Int, List<GameObject>> flourishGrid;

    private void Start()
    {
        playerPrefab = GameObject.FindWithTag("Player");
        player = playerPrefab.transform;

        backgroundPool = CreateBackgroundObjectPool(backgroundSprites, poolSize);
        flourishPool = CreateObjectPool(flourishPrefabs, flourishSize);
        grid = new Dictionary<Vector2Int, GameObject>();
        flourishGrid = new Dictionary<Vector2Int, List<GameObject>>();

        Vector2Int playerStartingGridPosition = WorldToGridPosition(player.transform.position);
        UpdateGrid(playerStartingGridPosition);
    }

    private void Update()
    {
        Vector2Int playerGridPosition = WorldToGridPosition(player.position);
        UpdateGrid(playerGridPosition);
    }


    private Queue<GameObject> CreateBackgroundObjectPool(Sprite[] sprites, int size)
    {
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = new GameObject("Background");
            SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

            // Apply the colorTint to the spriteRenderer's color
            spriteRenderer.color = colorTint;

            spriteRenderer.sortingOrder = -10;
            obj.SetActive(false);
            pool.Enqueue(obj);

            if (i == 0)
            {
                backgroundSize = spriteRenderer.bounds.size;
            }
        }

        return pool;
    }


    private Queue<GameObject> CreateObjectPool(GameObject[] prefabs, int size)
    {
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        return pool;
    }

    private Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.FloorToInt((worldPosition.x - backgroundSize.x / 2) / backgroundSize.x),
            Mathf.FloorToInt((worldPosition.y - backgroundSize.y / 2) / backgroundSize.y)
        );
    }

    private void UpdateGrid(Vector2Int playerGridPosition)
    {
        HashSet<Vector2Int> updatedPositions = new HashSet<Vector2Int>();

        for (int x = playerGridPosition.x - gridSize; x <= playerGridPosition.x + gridSize; x++)
        {
            for (int y = playerGridPosition.y - gridSize; y <= playerGridPosition.y + gridSize; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                updatedPositions.Add(gridPosition);

                if (!grid.ContainsKey(gridPosition))
                {
                    if (backgroundPool.Count == 0)
                    {
                        Vector2Int farthestKey = FindFarthestGridPosition(playerGridPosition);
                        GameObject farthestBackground = grid[farthestKey];
                        backgroundPool.Enqueue(farthestBackground);
                        farthestBackground.SetActive(false);
                        grid.Remove(farthestKey);
                        if (flourishGrid.ContainsKey(farthestKey))
                        {
                            List<GameObject> farthestFlourishes = flourishGrid[farthestKey];
                            foreach (GameObject flourish in farthestFlourishes)
                            {
                                flourish.SetActive(false);
                                flourishPool.Enqueue(flourish);
                            }
                            flourishGrid.Remove(farthestKey);
                        }
                    }

                    GameObject background = backgroundPool.Dequeue();
                    background.SetActive(true);
                    background.transform.position = new Vector3(
                        (x - 0.5f) * backgroundSize.x, (y - 0.5f) * backgroundSize.y, 0
                    );
                    grid[gridPosition] = background;

                    SpawnFlourishes(gridPosition);
                }
            }
        }
    }

    private Vector2Int FindFarthestGridPosition(Vector2Int playerGridPosition)
    {
        Vector2Int farthestKey = Vector2Int.zero;
        float maxDistance = 0;

        foreach (var key in grid.Keys)
        {
            float distance = Vector2.Distance(playerGridPosition, key);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestKey = key;
            }
        }

        return farthestKey;
    }
    private void SpawnFlourishes(Vector2Int gridPosition)
    {
        List<GameObject> flourishes = new List<GameObject>();

        for (int i = 0; i < Random.Range(flourishMin, flourishMax); i++)
        {
            if (flourishPool.Count == 0)
            {
                GameObject newObj = CreateFlourishObject();
                flourishPool.Enqueue(newObj);
            }

            GameObject flourish = flourishPool.Dequeue();
            flourish.SetActive(true);

            Vector3 spawnPosition = new Vector3(
                (gridPosition.x - 0.5f) * backgroundSize.x + Random.Range(0, backgroundSize.x),
                (gridPosition.y - 0.5f) * backgroundSize.y + Random.Range(0, backgroundSize.y),
                0);

            flourish.transform.position = spawnPosition;
            flourishes.Add(flourish);
        }

        flourishGrid[gridPosition] = flourishes;
    }

    private GameObject CreateFlourishObject()
    {
        GameObject obj = Instantiate(flourishPrefabs[Random.Range(0, flourishPrefabs.Length)]);
        obj.SetActive(false);

        return obj;
    }
}
