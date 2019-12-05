using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAAGuns : WorldEntity
{
    public int numberOfGunsToSpawn;
    public SpawnableObject gun;
    private Transform gunParent;

    private List<Vector2> spawnLocations = new List<Vector2>();

    private bool spawnGuns = false;

    public void SpawnGuns()
    {
        spawnGuns = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gunParent = this.gameObject.transform.Find("AA Guns");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnGuns)
        {
            for (int i = 0; i < numberOfGunsToSpawn; i++)
            {
                SpawnGun();
            }

            spawnGuns = false;
        }
    }

    private void SpawnGun()
    {
        Vector2 spawnLocation = new Vector2(Random.Range(WorldCenter.x - WorldLength, WorldCenter.x + WorldLength), Random.Range(WorldCenter.y - WorldLength, WorldCenter.y + WorldLength));
        float gunRadius = gun.spawnPrefab.GetComponent<SphereCollider>().radius;

        Debug.Log($"{spawnLocation}; {gunRadius}");

        bool foundLocation = false;
        while (!foundLocation)
        {
            bool notColliding = true;
            foreach (Vector2 point in spawnLocations)
            {
                float distance = this.GetComponent<TerrainObjectSpawner>().PythagoreanTheorem(spawnLocation, point);
                Debug.Log($"Distance: {distance}, Point: {point}, {distance + gunRadius <= gunRadius}");
                // if distance is <= radius it is inside or on the radius of the building
                if (distance <= gunRadius)
                {
                    spawnLocation = this.GetComponent<TerrainObjectSpawner>().GetLocationNotInWater(spawnLocation, gunRadius);
                    notColliding = false;

                    Debug.Log($"NewSpawn: {spawnLocation}");
                }
            }

            if (notColliding) foundLocation = true;
        }

        if (foundLocation)
        {
            // get y-axis var at spawnLocation
            float yAxis = this.GetComponent<TerrainObjectSpawner>().GetTerrainYaxis(new Vector3(spawnLocation.x, this.transform.position.y, spawnLocation.y));
            Vector3 spawnVector3 = new Vector3(spawnLocation.x, yAxis, spawnLocation.y);

            // spawn the buidling
            GameObject newBuilding = Instantiate(gun.spawnPrefab, spawnVector3, Quaternion.identity, gunParent);

            spawnLocations.Add(spawnLocation);

            //Debug.Log($"Spawning {buildings[buildingIndex].spawnPrefab} at {spawnVector3} rotated {rotation}");
        }
    }
}
