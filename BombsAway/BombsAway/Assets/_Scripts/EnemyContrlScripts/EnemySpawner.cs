using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
scrip takes care of the randomness - so we dont have to pass variables
simple enemy command script

random spawn
altitude height of player
direction to player from enemy - const update
desired speed (random)

gets to close? go over and fly away and turn around a little bit later to re-attack

damageable entity - script
take damage - health stuffs

just fly towards player for now

use shaun's script - flying script

*/

public class EnemySpawner : MonoBehaviour
{
    public float worldXsize;     // change later to get from world script
    public float worldZsize;
    public int timeBetweenSpawn;
    public EnemySpawn[] Enemies;

    private float deltaTime = 100f;
    private float totalWeightedProb = 0;

    // Start is called before the first frame update
    void Start()
    {
        // for now, spawn off the size of the map
        // in any radial direction
        foreach (EnemySpawn enemy in Enemies)
        {
            totalWeightedProb = enemy.CalculateWeightedSpawnProbability(totalWeightedProb);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deltaTime >= timeBetweenSpawn)
        {
            SpawnEnemy();
            deltaTime = 0;
        }
        else
        {
            deltaTime += Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        // choose random enemy from list, affected by probabilities of spawn
        // choose random location to spawn
        // choose random altitude?

        float randomEnemy = Random.Range(0, totalWeightedProb);
        foreach (EnemySpawn enemy in Enemies)
        {
            // spawn that enemy
            if (enemy.ProbWithinRange(randomEnemy))
            {
                // get altitude of player and spawn enemy at that altitude
                float playerYaltitude = GameObject.FindWithTag("Player").transform.position.y;
                Vector3 position = new Vector3(worldXsize, playerYaltitude, worldZsize);
                // find direction of player and point that way
                Vector3 lookingDirection = (GameObject.FindWithTag("Player").transform.position - position);
                Quaternion rotation = Quaternion.LookRotation(lookingDirection, Vector3.up);
                rotation.Set(0, rotation.y, 0, rotation.w);    // flatten the x and z rotation out
                // add 90 degrees x to make enemy parallel to ground
                //rotation *= Quaternion.Euler(90, 0, 0);     // MIGHT NOT HAVE TO DO THIS BASED ON THE MODEL
                Debug.Log($"Spawning: {enemy.EnemyPrefab} with probability of {enemy.probabilityOfSpawn}% from random number ({randomEnemy})\nWith position {position} and rotation {rotation}");
                Instantiate(enemy.EnemyPrefab, position, rotation, this.transform);

                break;
            }
        }
        // if rand prob not high enough, do not spawn anyone
    }

    // does not 100% work 😅
    private void OnValidate()
    {
        float probSum = 0;
        for (int i = 0; i < Enemies.Length; i++)
        {
            probSum += Enemies[i].probabilityOfSpawn;
            if (probSum > 1)
            {
                Enemies[i].probabilityOfSpawn -= (probSum - 1);
                break;
            }
        }

    }
}

[System.Serializable]
public class EnemySpawn
{

    public GameObject EnemyPrefab;
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
