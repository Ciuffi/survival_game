using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfinderFollow : MonoBehaviour
{
    private GameObject player;
    public AstarPath astarPath;
    public float rebuildDistance = 100f;

    private GridGraph gridGraph;
    private Bounds graphBounds;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        astarPath = FindObjectOfType<AstarPath>();
        gridGraph = astarPath.data.gridGraph;
        UpdateGraphBounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (gridGraph != null)
        {
            CheckPlayerDistance();
        }
    }

    private void CheckPlayerDistance()
    {
        Vector3 playerPosition = player.transform.position;
        float distanceToEdgeX = Mathf.Min(
            Mathf.Abs(playerPosition.x - graphBounds.min.x),
            Mathf.Abs(playerPosition.x - graphBounds.max.x)
        );

        float distanceToEdgeY = Mathf.Min(
            Mathf.Abs(playerPosition.y - graphBounds.min.y),
            Mathf.Abs(playerPosition.y - graphBounds.max.y)
        );

        if (distanceToEdgeX < rebuildDistance || distanceToEdgeY < rebuildDistance)
        {
            Vector3 newCenter = new Vector3(playerPosition.x, playerPosition.y, gridGraph.center.z);
            gridGraph.center = newCenter;
            astarPath.Scan();
            UpdateGraphBounds();
        }
    }

    private void UpdateGraphBounds()
    {
        Vector3 graphSize = new Vector3(gridGraph.width * gridGraph.nodeSize, gridGraph.depth * gridGraph.nodeSize, 0);
        graphBounds = new Bounds(gridGraph.center, graphSize);
    }

}
