using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : WorldEntity
{
    // spawn cities as grids
    public bool city;
    public int numberOfCities;
    public SpawnableObject[] cityLayouts;
    public SpawnableObject[] buildings;

    private int mapGeneratorTerrainIndex = -1;

    //private int smallCity = 20;
    //private int mediumCity = 30;
    //private int bigCity = 60;

    private Transform buildingParent;

    //private int gridSquareSize = 600;
    private float lowestYpoint;
    private float[] rotationIncrements = { 0f, 90f, 180f, 360f };

    private EStatus buildingGenerationStatus = EStatus.hasNotStarted;
    private float totalWeightedProbBuildings = 0;
    private float totalWeightedProbCities = 0;

    public LayerMask layerMask;

    public EStatus GetBuildingGenerationStatus()
    {
        return buildingGenerationStatus;
    }

    //---------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        if (city) buildingParent = this.gameObject.transform.Find("Buildings");
        else buildingParent = this.gameObject.transform.Find("Shrubery");
        lowestYpoint = GameObject.FindWithTag("Water").transform.position.y;

        // calculate the weighted prob for buildings
        foreach (SpawnableObject buidling in buildings)
        {
            totalWeightedProbBuildings = buidling.CalculateWeightedSpawnProbability(totalWeightedProbBuildings);
        }

        foreach (SpawnableObject city in cityLayouts)
        {
            totalWeightedProbCities = city.CalculateWeightedSpawnProbability(totalWeightedProbCities);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingGenerationStatus == EStatus.hasNotStarted)
        {
            // FIND SOMETHING BETTER
            GameObject instance = GameObject.FindWithTag("MapGenerator");
            if (instance.GetComponent<IslandGenerator>().FinishedTerrainGeneration()) buildingGenerationStatus = EStatus.start;
        }
        if (buildingGenerationStatus == EStatus.start)
        {
            GenerateCities();
            buildingGenerationStatus = EStatus.completed;
        }
    }

    private void GenerateCities()
    {
        for (int i = 0; i < numberOfCities; i++)
        {
            SpawnCity();
        }
    }

    private void SpawnCity()
    {
        // go through terrain chuncks
        // pick a layout
        // go through each placeholder buidling and place a random building
        // find y value of building

        if (city) UpdateTerrainIndex();
        else RandomTerrainIndex();
        Vector3 spawnSeed =  FindAcceptablePoint();

        int cityLayoutIndex = GetRandomCityLayoutIndex();
        Quaternion cityRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        GameObject cityLayout = Instantiate(cityLayouts[cityLayoutIndex].spawnPrefab, spawnSeed, cityRotation);

        int numOfBuildings = cityLayout.transform.childCount;

        for (int i = 0; i < numOfBuildings; ++i)
        {
            int buildingIndex = GetRandomBuidlingIndex();
            Vector3 placeholderPosition = cityLayout.transform.GetChild(i).transform.position;
            float yPosition = GetTerrianHeightAtPoint(placeholderPosition.x, placeholderPosition.z);
            Vector3 position = new Vector3(placeholderPosition.x, yPosition, placeholderPosition.z);
            if (LocationIsAcceptable(position))
            {
                // give the buidling some rotation
                Quaternion buildingRotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);
                GameObject newBuilding = Instantiate(buildings[buildingIndex].spawnPrefab, position, buildingRotation, buildingParent);
            }
        }

        // delete the city layout
        Destroy(cityLayout);
    }

    private void UpdateTerrainIndex()
    {
        if (mapGeneratorTerrainIndex < GameObject.FindWithTag("MapGenerator").transform.childCount - 1)
        {
            mapGeneratorTerrainIndex++;
        }
        else if (mapGeneratorTerrainIndex == GameObject.FindWithTag("MapGenerator").transform.childCount - 1)
        {
            mapGeneratorTerrainIndex = 0;   // reset the index and start over again
        }
    }

    private void RandomTerrainIndex()
    {
        mapGeneratorTerrainIndex = Random.Range(0, GameObject.FindWithTag("MapGenerator").transform.childCount - 1);
    }

    private Vector3 FindAcceptablePoint()
    {
        // find a point within the x,y range that is not in water
        Vector3 terrainCenter = GameObject.FindWithTag("MapGenerator").transform.GetChild(mapGeneratorTerrainIndex).transform.position;
        float maxX = terrainCenter.x + WorldLength / 2;
        float minX = terrainCenter.x - WorldLength / 2;
        float maxZ = terrainCenter.z + WorldLength / 2;
        float minZ = terrainCenter.z - WorldLength / 2;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        float Y = GetTerrianHeightAtPoint(randomX, randomZ);

        return new Vector3(randomX, Y, randomZ);

    }

    private bool LocationIsAcceptable(Vector3 position)
    {
        if (position.y > lowestYpoint) return true;
        else return false;
    }

    private float GetTerrianHeightAtPoint(float x, float z)
    {
        Vector3 point = new Vector3(x, 0, z);
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, 10000, layerMask))
        {
            if (hit.point.y > lowestYpoint) return hit.point.y;
            else return -8000f; // in water
        }

        return -8000f;  // something went wrong
    }

    private int GetRandomCityLayoutIndex()
    {
        float randomCityProb = Random.Range(0, totalWeightedProbCities);
        for (int i = 0; i < cityLayouts.Length; i++)
        {
            // spawn that enemy if it is the proper weight
            if (cityLayouts[i].ProbWithinRange(randomCityProb))
            {
                return i;
            }
        }
        return -1;
    }

    private int GetRandomBuidlingIndex()
    {
        float randomBuildingProb = Random.Range(0, totalWeightedProbBuildings);
        for (int i = 0; i < buildings.Length; i++)
        {
            // spawn that enemy if it is the proper weight
            if (buildings[i].ProbWithinRange(randomBuildingProb))
            {
                return i;
            }
        }
        return -1;
    }
}
