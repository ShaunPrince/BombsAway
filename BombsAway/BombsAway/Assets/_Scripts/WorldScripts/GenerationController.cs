using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controlls the order of generation so that terrain, buildings, and AA guns should spawn the same with the same seed
public class GenerationController : MonoBehaviour
{
    private GameObject mapSpawn;
    private GameObject objectsSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        mapSpawn = GameObject.Find("MapGenerator");
        objectsSpawn = GameObject.Find("Buidling Spawner");

        // 1     Terrain
        // 2,3,4 Objects (buildings, plants, aaGuns)
        // 5     Enemies

        // enable the script in the game object one at a time
        // when it is done, trigger the next enable

        mapSpawn.GetComponent<IslandGenerator>().heightMapSettings.noiseSettings.seed = SeedRandomGeneration.GetRandomSeed();
    }
}
