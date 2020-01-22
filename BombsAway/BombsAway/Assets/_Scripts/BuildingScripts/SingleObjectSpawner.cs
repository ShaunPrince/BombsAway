using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObjectSpawner : WorldEntity
{
    public int numberOfObjects;
    public SpawnableObject Object;
    private Transform objectParent;

    private float lowestYpoint;
    private int mapGeneratorTerrainIndex = 0;
    private int spawnIndex = 0;

    private EStatus generationStatus = EStatus.hasNotStarted;

    public LayerMask layerMask;

    public EStatus GetObjectGenerationStatus()
    {
        return generationStatus;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectParent = this.gameObject.transform.Find("AA Guns");
        lowestYpoint = GameObject.FindWithTag("Water").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (generationStatus == EStatus.hasNotStarted)
        {
            // FIND SOMETHING BETTER
            GameObject instance = GameObject.FindWithTag("MapGenerator");
            if (instance.GetComponent<IslandGenerator>().FinishedTerrainGeneration()) generationStatus = EStatus.start;
        }
        if (generationStatus == EStatus.start)
        {
            GenerateObjects();
            generationStatus = EStatus.completed;
        }
    }

    private void GenerateObjects()
    {
        for (spawnIndex = 0; spawnIndex < numberOfObjects; spawnIndex++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 position = FindAcceptablePoint();

        if (LocationIsAcceptable(position))
        {
            //position.y = position.y + 50f;
            // give the buidling some rotation
            //Quaternion buildingRotation = Quaternion.Euler(0, rotationIncrements[Random.Range(0, rotationIncrements.Length)], 0);
            GameObject newObject = Instantiate(Object.spawnPrefab, position, Quaternion.identity, objectParent);
            UpdateTerrainIndex();
        }
        // if not acceptable position, try again
        else
        {
            spawnIndex--;
            //UndoTerrainIndexUpdate();
        }
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

    private void UpdateTerrainIndex()
    {
        Debug.Log($"{GameObject.FindWithTag("MapGenerator").transform.childCount}");
        if (mapGeneratorTerrainIndex < GameObject.FindWithTag("MapGenerator").transform.childCount - 1)
        {
            mapGeneratorTerrainIndex++;
        }
        else if (mapGeneratorTerrainIndex == GameObject.FindWithTag("MapGenerator").transform.childCount - 1)
        {
            mapGeneratorTerrainIndex = 0;   // reset the index and start over again
        }
    }

    private void UndoTerrainIndexUpdate()
    {
        if (mapGeneratorTerrainIndex > 0)
        {
            mapGeneratorTerrainIndex--;
        }
        else if (mapGeneratorTerrainIndex == 0)
        {
            mapGeneratorTerrainIndex = GameObject.FindWithTag("MapGenerator").transform.childCount-1;   // reset the index and start over again
        }
    }

    private bool LocationIsAcceptable(Vector3 position)
    {
        if (position.y > lowestYpoint) return true;
        else return false;
    }
}
