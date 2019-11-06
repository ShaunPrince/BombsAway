using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject spawnPrefab;
    [Range(0, 1)]
    public float probabilityOfSpawn;

    private float weightedSpawn;

    public float CalculateWeightedSpawnProbability(float sum)
    {
        weightedSpawn = sum + probabilityOfSpawn;
        return weightedSpawn;
    }

    public bool ProbWithinRange(float probablity)
    {
        if (weightedSpawn >= probablity) return true;
        return false;
    }

}
