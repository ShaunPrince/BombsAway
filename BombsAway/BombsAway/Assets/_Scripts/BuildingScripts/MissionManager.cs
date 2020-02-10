using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Choose N-number buildings, mark them as targets
 * Limit what buildings can be targets?
 * 
 * Static playerScore, when a building dies, add the value to playerScore
 */

public class MissionManager : WorldEntity
{
    public static int numberOfTargetBuildings = 5;
    private static int numberRemainingTargets;
    public static int playerScore = 0; // initialize to zero

    private GameObject buildingSpawner;
    private Transform playerTransform;
    private bool buildingTargetingCompleted = false;

    private static bool playerInBounds = true;

    // Start is called before the first frame update
    void Start()
    {
        buildingSpawner = GameObject.FindWithTag("BuildingSpawner");
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        numberRemainingTargets = numberOfTargetBuildings;
    }

    // Update is called once per frame
    void Update()
    {
        // if the buuldings are done spawning, randomly select N-number to be target
        if (buildingSpawner.GetComponentInParent<ObjectSpawner>().GetBuildingGenerationStatus() == EStatus.completed && !buildingTargetingCompleted)
        {
            HashSet<int> alreadyChosenBuilding = new HashSet<int>();

            for (int i = 0; i < numberOfTargetBuildings; i++)
            {
                int randomChildIdx = Random.Range(0, buildingSpawner.transform.childCount);
                while (alreadyChosenBuilding.Contains(randomChildIdx))
                {
                    randomChildIdx = Random.Range(0, buildingSpawner.transform.childCount);
                }

                alreadyChosenBuilding.Add(randomChildIdx);
                Transform randomChildBuilding = buildingSpawner.transform.GetChild(randomChildIdx);
                randomChildBuilding.GetComponent<TerrainObject>().objectType = ETerrainObjectType.Target;
            }

            buildingTargetingCompleted = true;
        }

        // check if the player is still within bounds of the map
        if (Mathf.Abs(playerTransform.position.x) > WorldLength || Mathf.Abs(playerTransform.position.z) > WorldLength)
        {
            playerInBounds = false;
        }
        else
        {
            playerInBounds = true;
        }
    }

    public static void IncreasePlayerScore(int incrementAmount)
    {
        playerScore += incrementAmount;
        //Debug.Log($"Player score: {playerScore}");
    }

    public static void DecreaseTargetCount()
    {
        numberRemainingTargets--;
    }

    public static int NumberOfRemainingTargets()
    {
        return numberRemainingTargets;
    }

    public static bool PlayerInBounds()
    {
        return playerInBounds;
    }

    public static bool HasPlayerWon()
    {
        if (numberRemainingTargets <= 0) return true;
        else return false;
    }
}
