using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// take in a list of buildings
// randomly choose one
// spawn it
    // more likely to be spawned next to another building, but not too close
    // find the y position from the terrain at that point

public class TerrainObjectSpawner : WorldEntity
{
    public int numberOfCitiesToSpawn;
    public int maxCitySize;
    public int numberOfBuildingsToSpawn;
    public SpawnableObject[] buildings;
    private Transform buildingParent;

    public int numberOfShruberiesToSpawn;
    public SpawnableObject[] shrubery;
    private Transform shruberyParent;

    private List<Vector2> spawnLocations = new List<Vector2>();
    private Dictionary<Vector2, GameObject> spawnDictionary = new Dictionary<Vector2, GameObject>();

    private float totalWeightedProbBuildings = 0;
    private EStatus buildingGenerationStatus = EStatus.hasNotStarted;

    private float totalWeightedProbShrubery = 0;

    private float[] rotationIncrements = { 0f, 90f, 180f, 360f };

    private bool firstCity = true;

    public void GenerateBuildings()
    {
        for (int i = 0; i < numberOfCitiesToSpawn; i++)
        {
            SpawnCities();
        }

        for (int i = 0; i < numberOfBuildingsToSpawn; i++)
        {
            SpawnSemiRandomObject(buildings, totalWeightedProbBuildings, buildingParent);
        }

        for (int i = 0; i < numberOfShruberiesToSpawn; i++)
        {
            SpawnSemiRandomObject(shrubery, totalWeightedProbShrubery, shruberyParent);
        }
    }

    public EStatus GetBuildingGenerationStatus()
    {
        return buildingGenerationStatus;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingParent = this.gameObject.transform.Find("Buildings");
        shruberyParent = this.gameObject.transform.Find("Shrubery");

        // calculate the weighted prob for buildings
        foreach (SpawnableObject buidling in buildings)
        {
            totalWeightedProbBuildings = buidling.CalculateWeightedSpawnProbability(totalWeightedProbBuildings);
        }

        // calculate the weighted prob for shrubery
        foreach (SpawnableObject plant in shrubery)
        {
            totalWeightedProbShrubery = plant.CalculateWeightedSpawnProbability(totalWeightedProbShrubery);
        }
    }

    private void Update()
    {
        if (buildingGenerationStatus == EStatus.hasNotStarted)
        {  
            // FIND SOMETHING BETTER
            GameObject instance = GameObject.FindWithTag("MapGenerator");
            if (instance.GetComponent<IslandGenerator>().FinishedTerrainGeneration()) buildingGenerationStatus = EStatus.start;
        }
        if (buildingGenerationStatus == EStatus.start)
        {
            GenerateBuildings();
            buildingGenerationStatus = EStatus.completed;
        }
    }

    private void SpawnCities()
    {
        int citySize = Mathf.FloorToInt(Random.Range(1, maxCitySize));
        // spawn first city at player's location
        Vector2 citySeed;
        if (firstCity)
        {
            citySeed = new Vector2(0, 0);
            firstCity = false;
        }
        else citySeed = new Vector2(Random.Range(-WorldLength, WorldLength), Random.Range(-WorldLength, WorldLength));
        float cityRadius = 0;

        // for each ring in the city
        for (int i = 0; i < citySize; i++)
        {
            // less buildings closer to the center of the city
            int numBuildingsInRing = Mathf.FloorToInt(Random.Range(1, numberOfBuildingsToSpawn));
            for (int b = 0; b < numBuildingsInRing; b++)
            {
                // get a random building
                int buildingIndex = GetBuildingIndex(Random.Range(0, totalWeightedProbBuildings));
                float buildingRadius = buildings[buildingIndex].spawnPrefab.GetComponent<SphereCollider>().radius;

                float angle = Random.Range(0, 360);
                float collidingRadius;

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
                            //spawnLocation = GetLocationNotInWater(spawnLocation, buildingRadius);

                            collidingRadius = spawnDictionary[spawnLocation].GetComponent<SphereCollider>().radius;

                            angle = Random.Range(0, 360);

                            spawnLocation.x = spawnLocation.x + collidingRadius * Mathf.Cos(angle);
                            spawnLocation.y = spawnLocation.y + collidingRadius * Mathf.Sin(angle);
                            notColliding = false;
                        }
                    }

                    if (spawnLocation.x == float.MaxValue || spawnLocation.y == float.MaxValue)
                    {
                        // could not find a location, do not spawn
                        break;
                    }

                    if (notColliding)
                    {
                        foundLocation = true;
                    }
                }

                if (spawnLocation.x != float.MaxValue || spawnLocation.y != float.MaxValue)
                {
                    // get y-axis var at spawnLocation
                    float yAxis = GetTerrainYaxis(new Vector3(spawnLocation.x, this.transform.position.y, spawnLocation.y));
                    Vector3 spawnVector3 = new Vector3(spawnLocation.x, yAxis, spawnLocation.y);

                    // give the buidling some rotation?
                    Quaternion rotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);

                    // spawn the buidling
                    GameObject newBuilding = Instantiate(buildings[buildingIndex].spawnPrefab, spawnVector3, rotation, buildingParent);

                    spawnDictionary.Add(spawnLocation, newBuilding);

                    //Debug.Log($"Spawning {buildings[buildingIndex].spawnPrefab} at {spawnVector3} rotated {rotation}");
                }
            }

            // choose a location on the first ring of the city
            cityRadius += buildings[Random.Range(0, buildings.Length)].spawnPrefab.GetComponent<SphereCollider>().radius * 2;
        }
    }

    Vector2 GetLocationNotInWater(Vector2 spawnLocation, float buildingRadius)
    {
        float yAxis = 0;
        int numOfAttemps = 0;
        int maxNumOfAttemps = 10;
        int totalNumOfAttemps = 0;
        int cutOffAttemps = 100;
        float radiusIncrease = 5f;
        while (yAxis <= -4000)
        {
            // if keep trying and all locations are y = 0; check in a wider radius
            if (totalNumOfAttemps > cutOffAttemps)
            {
                spawnLocation = new Vector2(float.MaxValue, float.MaxValue);
                break;
            }

            if (numOfAttemps > maxNumOfAttemps)
            {
                buildingRadius += radiusIncrease;
                numOfAttemps = 0;
            }

            float angle = Random.Range(0, 360);

            spawnLocation.x = spawnLocation.x + buildingRadius * Mathf.Cos(angle);
            spawnLocation.y = spawnLocation.y + buildingRadius * Mathf.Sin(angle);

            yAxis = GetTerrainYaxis(new Vector3(spawnLocation.x, this.transform.position.y, spawnLocation.y));

            numOfAttemps++;
            totalNumOfAttemps++;
        }

        return spawnLocation;
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

    private void SpawnSemiRandomObject(SpawnableObject[] objectArray, float totalProb, Transform parent)
    {
        float randomObject = Random.Range(0, totalProb);

        foreach (SpawnableObject item in objectArray)
        {
            // spawn that enemy if it is the proper weight
            if (item.ProbWithinRange(randomObject))
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
                    float buildingRadius = item.spawnPrefab.GetComponent<SphereCollider>().radius;
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
                            break;
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
                float yAxis = GetTerrainYaxis(new Vector3(spawnLocation.x, this.transform.position.y, spawnLocation.y));
                Vector3 spawnVector3 = new Vector3(spawnLocation.x, yAxis, spawnLocation.y);

                // give the buidling some rotation?
                Quaternion rotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);

                // spawn the buidling
                GameObject newObject = Instantiate(item.spawnPrefab, spawnVector3, rotation, parent);

                spawnDictionary.Add(spawnLocation, newObject);

                //Debug.Log($"Spawning {building.spawnPrefab} at {spawnVector3} rotated {rotation}");

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
        RaycastHit[] hit = Physics.RaycastAll(spawnPoint, Vector3.down, 5000);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.layer == 9)
            {
                return hit[i].point.y;
            }
        }

        // something went wrong
        return -5000f;
    }

    private void OnValidate()
    {
        if (maxCitySize < 1) maxCitySize = 1;
    }
}