using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGen : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;


    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProcGen();
    }

    protected abstract void RunProcGen();
}
