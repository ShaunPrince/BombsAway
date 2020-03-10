using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SeedRandomGeneration
{
    private static int randomSeed = Random.Range(-10000, 10000);

    private static void Awake()
    {
        //randomSeed = Random.Range(-10000, 10000);
        //randomSeed = 2;
    }
    
    public static int GetRandomSeed()
    {
        return randomSeed;
    }
}
