using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// take in a list of buildings
// randomly choose one
// spawn it
    // more likely to be spawned next to another building, but not too close
    // find the y position from the terrain at that point

public class BuildingSpawner : WorldEntity
{
    public int numberOfCitiesToSpawn;
    public int maxCitySize;
    public int numberOfBuildingsToSpawn;
    public SpawnableObject[] buildings;

    private List<Vector2> spawnLocations = new List<Vector2>();
    private Dictionary<Vector2, GameObject> spawnDictionary = new Dictionary<Vector2, GameObject>();

    private float totalWeightedProb = 0;
    private Estatus generateBuildings = Estatus.hasNotStarted;

    private float[] rotationIncrements = { 0f, 90f, 180f, 360f };

    // Start is called before the first frame update
    void Start()
    {

        // calculate the weighted prob
        foreach (SpawnableObject buidling in buildings)
        {
            totalWeightedProb = buidling.CalculateWeightedSpawnProbability(totalWeightedProb);
        }
    }

    private void Update()
    {
        if (generateBuildings == Estatus.hasNotStarted)
        {  
            // FIND SOMETHING BETTER
            GameObject instance = GameObject.FindWithTag("MapGenerator");
            if (instance.GetComponent<ThreadedDataRequester>().FinishedTerrainGeneration()) generateBuildings = Estatus.start;
        }
        if (generateBuildings == Estatus.start)
        {
            GenerateBuildings();
            generateBuildings = Estatus.completed;
        }
    }

    public void GenerateBuildings()
    {
        for (int i = 0; i < numberOfCitiesToSpawn; i++)
        {
            SpawnCities();
        }

        for (int i = 0; i < numberOfBuildingsToSpawn; i++)
        {
            SpawnSemiRandomBuildings();
        }
    }

    private void SpawnCities()
    {
        int citySize = Mathf.FloorToInt(Random.Range(1, maxCitySize));
        Vector2 citySeed = new Vector2(Random.Range(-WorldLength, WorldLength), Random.Range(-WorldLength, WorldLength));
        float cityRadius = 0;

        // for each ring in the city
        for (int i = 0; i < citySize; i++)
        {
            // less buildings closer to the center of the city
            int numBuildingsInRing = Mathf.FloorToInt(Random.Range(1, numberOfBuildingsToSpawn));
            for (int b = 0; b < numBuildingsInRing; b++)
            {
                // get a random building
                int buildingIndex = GetBuildingIndex(Random.Range(0, totalWeightedProb));
                float buildingRadius = buildings[buildingIndex].spawnPrefab.GetComponent<SphereCollider>().radius;

                float angle = Random.Range(0, 360);

                Vector2 spawnLocation = new Vector2(citySeed.x + cityRadius * Mathf.Cos(angle), citySeed.y + cityRadius * Mathf.Sin(angle));

                // make sure no other building is with in the radius of current building
                bool foundLocation = false;
                while (!foundLocation)
                {
                    bool notColliding = true;
                    foreach (Vector2 point in spawnDictionary.Keys)
                    {
                        float distance = PythagoreanTheorem(spawnLocation, point);
                        // if distance is <= radius it is inside or on the radius of the building
                        if (distance <= buildingRadius)
                        {
                            angle = Random.Range(0, 360);

                            spawnLocation.x = spawnLocation.x + buildingRadius * Mathf.Cos(angle);
                            spawnLocation.y = spawnLocation.y + buildingRadius * Mathf.Sin(angle);
                            notColliding = false;
                        }
                    }

                    if (notColliding)
                    {
                        foundLocation = true;
                    }
                }

                // get y-axis var at spawnLocation
                float yAxis = GetTerrainYaxis(new Vector3(spawnLocation.x, 0f, spawnLocation.y));
                Vector3 spawnVector3 = new Vector3(spawnLocation.x, yAxis, spawnLocation.y);

                // give the buidling some rotation?
                Quaternion rotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);

                // spawn the buidling
                GameObject newBuilding = Instantiate(buildings[buildingIndex].spawnPrefab, spawnVector3, rotation, this.transform);

                spawnDictionary.Add(spawnLocation, newBuilding);

                Debug.Log($"Spawning {buildings[buildingIndex].spawnPrefab} at {spawnVector3} rotated {rotation}");
            }

            // choose a location on the first ring of the city
            cityRadius += buildings[Random.Range(0, buildings.Length)].spawnPrefab.GetComponent<SphereCollider>().radius * 2;
        }
    }

    private int GetBuildingIndex(float buildingProbability)
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            // spawn that enemy if it is the proper weight
            if (buildings[i].ProbWithinRange(buildingProbability))
            {
                return i;
            }
        }
        return -1;
    }

    private void SpawnSemiRandomBuildings()
    {
        float randomBuilding = Random.Range(0, totalWeightedProb);

        foreach (SpawnableObject building in buildings)
        {
            // spawn that enemy if it is the proper weight
            if (building.ProbWithinRange(randomBuilding))
            {
                // choose random x,z within map size
                Vector2 randomLocation = new Vector2(Random.Range(-WorldLength, WorldLength), Random.Range(-WorldLength, WorldLength));
                spawnLocations.Add(randomLocation);

                Vector2 spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
                // if that location already has a building, find spot outlisde of radius

                bool foundLocation = false;
                while (!foundLocation)
                {
                    float collidingRadius;
                    float angle;
                    // Check if that spawnLocation is already in use
                    // if so, get the radius of the building in it's place and spawn outside of radius
                    if (spawnDictionary.ContainsKey(spawnLocation))
                    {
                        collidingRadius = spawnDictionary[spawnLocation].GetComponent<SphereCollider>().radius;

                        angle = Random.Range(0, 360);

                        spawnLocation.x = spawnLocation.x + collidingRadius * Mathf.Cos(angle);
                        spawnLocation.y = spawnLocation.y + collidingRadius * Mathf.Sin(angle);
                    }

                    // make sure no other building is with in the radius of current building too
                    // then add new location to List
                    float buildingRadius = building.spawnPrefab.GetComponent<SphereCollider>().radius;
                    bool notColliding = true;
                    foreach (Vector2 point in spawnDictionary.Keys)
                    {
                        float distance = PythagoreanTheorem(spawnLocation, point);
                        // if distance is <= radius it is inside or on the radius of the building
                        if (distance <= buildingRadius)
                        {
                            collidingRadius = spawnDictionary[point].GetComponent<SphereCollider>().radius;

                            angle = Random.Range(0, 360);

                            spawnLocation.x = spawnLocation.x + collidingRadius * Mathf.Cos(angle);
                            spawnLocation.y = spawnLocation.y + collidingRadius * Mathf.Sin(angle);
                            notColliding = false;
                        }
                    }

                    if (notColliding)
                    {
                        foundLocation = true;
                        // add the found location to the list of locations to spawn (and remove unused one)
                        //spawnLocations.RemoveAt(spawnLocations.Count - 1);
                        spawnLocations.Add(spawnLocation);
                    }
                }

                // get y-axis var at spawnLocation
                float yAxis = GetTerrainYaxis(new Vector3(spawnLocation.x, 0f, spawnLocation.y));
                Vector3 spawnVector3 = new Vector3(spawnLocation.x, yAxis, spawnLocation.y);

                // give the buidling some rotation?
                Quaternion rotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);

                // spawn the buidling
                GameObject newBuilding = Instantiate(building.spawnPrefab, spawnVector3, rotation, this.transform);

                spawnDictionary.Add(spawnLocation, newBuilding);

                Debug.Log($"Spawning {building.spawnPrefab} at {spawnVector3} rotated {rotation}");

                break;
            }

        }
    }

    private float PythagoreanTheorem(Vector2 point1, Vector2 point2)
    {
        float x = Mathf.Pow((point2.x - point1.x), 2);
        float y = Mathf.Pow((point2.y - point1.y), 2);
        return Mathf.Sqrt(x + y);
    }

    private float GetTerrainYaxis(Vector3 spawnPoint)
    {
        // shoot a raycast from the bottom of the building
        // find at what y point it hits the terrain below it
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint, Vector3.down, out hit))
        {
            return hit.point.y;
        }

        // something went wrong
        return 0f;
    }

    private void OnValidate()
    {
        if (maxCitySize < 1) maxCitySize = 1;
    }
}

enum Estatus
{
    hasNotStarted,
    start,
    completed
}