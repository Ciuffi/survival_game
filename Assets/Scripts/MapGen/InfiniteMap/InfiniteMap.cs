using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [SerializeField] private Color colorTint;
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private GameObject[] flourishPrefabs;
    [SerializeField] private int poolSize = 10;

    [SerializeField] private int gridSize = 5;

    private GameObject playerPrefab;
    private Transform player;
    private Vector2 backgroundSize;
    private Queue<GameObject> backgroundPool;
    private ObjectPool flourishPool, rockPool, ore1Pool, ore2Pool, torchPool;
    public int flourishMin = 1, flourishMax = 3;
    public int rockMin = 1, rockMax = 3;
    public int ore1Min = 1, ore1Max = 3;
    public int ore2Min = 1, ore2Max = 3;
    public int torchMin = 1, torchMax = 3;

    public GameObject[] rockPrefabs;
    public float[] rockWeights;
    public GameObject[] ore1Prefabs;
    public float[] ore1Weights;
    public GameObject[] ore2Prefabs;
    public float[] ore2Weights;

    public GameObject[] torchPrefabs;
    public float[] torchWeights;

    private Dictionary<Vector2Int, GameObject> grid;
    private Dictionary<Vector2Int, List<GameObject>> flourishGrid, rockGrid, torchGrid, ore1Grid, ore2Grid;

    private void Start()
    {
        playerPrefab = GameObject.FindWithTag("Player");
        player = playerPrefab.transform;

        backgroundPool = CreateBackgroundObjectPool(backgroundSprites, poolSize);
        flourishPool = new ObjectPool(flourishPrefabs, new float[flourishPrefabs.Length].Select(_ => 1f).ToArray(), poolSize);
        rockPool = new ObjectPool(rockPrefabs, rockWeights, rockPrefabs.Length);
        torchPool = new ObjectPool(torchPrefabs, torchWeights, torchPrefabs.Length);
        ore1Pool = new ObjectPool(ore1Prefabs, ore1Weights, ore1Prefabs.Length);
        ore2Pool = new ObjectPool(ore2Prefabs, ore2Weights, ore2Prefabs.Length);

        grid = new Dictionary<Vector2Int, GameObject>();
        flourishGrid = new Dictionary<Vector2Int, List<GameObject>>();
        rockGrid = new Dictionary<Vector2Int, List<GameObject>>();
        ore1Grid = new Dictionary<Vector2Int, List<GameObject>>();
        ore2Grid = new Dictionary<Vector2Int, List<GameObject>>();
        torchGrid = new Dictionary<Vector2Int, List<GameObject>>();

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
                        RemoveObjectsFromGrid(farthestKey, flourishGrid, flourishPool);
                        RemoveObjectsFromGrid(farthestKey, rockGrid, rockPool);
                        RemoveObjectsFromGrid(farthestKey, ore1Grid, ore1Pool);
                        RemoveObjectsFromGrid(farthestKey, ore2Grid, ore2Pool);
                        RemoveObjectsFromGrid(farthestKey, torchGrid, torchPool);
                    }

                    GameObject background = backgroundPool.Dequeue();
                    background.SetActive(true);
                    background.transform.position = new Vector3(
                        (x - 0.5f) * backgroundSize.x, (y - 0.5f) * backgroundSize.y, 0
                    );
                    grid[gridPosition] = background;

                    SpawnObjects(gridPosition, flourishPool, flourishMin, flourishMax, flourishGrid);
                    SpawnObjects(gridPosition, rockPool, rockMin, rockMax, rockGrid);
                    SpawnObjects(gridPosition, ore1Pool, ore1Min, ore1Max, ore1Grid);
                    SpawnObjects(gridPosition, ore2Pool, ore2Min, ore2Max, ore2Grid);
                    SpawnObjects(gridPosition, torchPool, torchMin, torchMax, torchGrid);
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

    private void SpawnObjects(Vector2Int gridPosition, ObjectPool pool, int min, int max, Dictionary<Vector2Int, List<GameObject>> gridDict)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();

        for (int i = 0; i < Random.Range(min, max); i++)
        {
            GameObject obj = pool.GetObject();
            obj.SetActive(true);
            Vector3 spawnPosition = new Vector3(
                (gridPosition.x - 0.5f) * backgroundSize.x + Random.Range(0, backgroundSize.x),
                (gridPosition.y - 0.5f) * backgroundSize.y + Random.Range(0, backgroundSize.y),
                0);
            obj.transform.position = spawnPosition;
            spawnedObjects.Add(obj);
        }

        gridDict[gridPosition] = spawnedObjects;
    }

    private void RemoveObjectsFromGrid(Vector2Int gridPosition, Dictionary<Vector2Int, List<GameObject>> gridDict, ObjectPool pool)
    {
        if (gridDict.TryGetValue(gridPosition, out List<GameObject> objects))
        {
            foreach (GameObject obj in objects)
            {
                pool.ReturnObject(obj);
            }
            gridDict.Remove(gridPosition);
        }
    }
}

public class ObjectPool
{
    private Queue<GameObject> poolQueue;
    private GameObject[] prefabs;
    private float[] weights;

    public ObjectPool(GameObject[] prefabs, float[] weights, int size)
    {
        this.prefabs = prefabs;
        this.weights = weights;
        poolQueue = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject chosenPrefab = ChooseByWeight();
            GameObject obj = GameObject.Instantiate(chosenPrefab);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    private GameObject ChooseByWeight()
    {
        float totalWeight = weights.Sum();
        float randomWeight = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        for (int i = 0; i < prefabs.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomWeight <= cumulativeWeight)
            {
                return prefabs[i];
            }
        }
        return prefabs[0];
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
            return poolQueue.Dequeue();
        else
            return GameObject.Instantiate(ChooseByWeight());
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }
}
