using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleWalkParameters_" ,menuName = "PCG/SimpleWalkData")]
public class SimpleWalkData : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = false;
}
