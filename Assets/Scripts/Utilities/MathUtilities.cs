using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class MathUtilities
{
    public static Vector3 DegreesToVector3(float degrees)
    {
        var radians = degrees * Mathf.Deg2Rad;
        var x = Mathf.Cos(radians);
        var y = Mathf.Sin(radians);
        return new Vector3(x, y, 0);
    }
    public static int GetWeightedResult(int[] weights)
    {
        int randomWeight = UnityEngine.Random.Range(0, weights.Sum());
        for (int i = 0; i < weights.Length; ++i)
        {
            randomWeight -= weights[i];
            if (randomWeight < 0)
            {
                return i;
            }
        }
        return weights.ToList().IndexOf(weights.Max());
    }
}
