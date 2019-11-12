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

public class EnemySpawner : WorldEntity
{
    public float distanceToSpawn;
    public float timeBetweenSpawn;
    public int minSpawnTime;
    [Range(0,1)]
    public float spawnTimeDecrement;
    //[Tooltip("Increase in speed that allows the enemy to catch up initially, >0")]
    //public float initialSpeedIncrease;
    public SpawnableObject[] Enemies;

    private float deltaTime = 100f;     // to create insta spawn
    private float totalWeightedProb = 0;

    private int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // calculate the weighted prob
        foreach (SpawnableObject enemy in Enemies)
        {
            totalWeightedProb = enemy.CalculateWeightedSpawnProbability(totalWeightedProb);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (deltaTime >= timeBetweenSpawn)
        {
            SpawnEnemy();
            
            if (timeBetweenSpawn - spawnTimeDecrement > minSpawnTime) timeBetweenSpawn -= spawnTimeDecrement;

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
        // choose random altitude (at same as player, gets affected later)

        float randomEnemy = Random.Range(0, totalWeightedProb);
        foreach (SpawnableObject enemy in Enemies)
        {
            // spawn that enemy if it is the proper weight
            if (enemy.ProbWithinRange(randomEnemy))
            {
                // get altitude of player and spawn enemy around that altitude
                float playerYaltitude = GameObject.FindWithTag("Player").transform.position.y;

                // get random location off the map (radially)
                // calculate circumfrence of the map, choose any point along it
                Vector2 xzPos = CalculateRandomSpawnLocation();

                Vector3 position = new Vector3(xzPos.x, playerYaltitude, xzPos.y);

                // find direction of player and point that way
                Vector3 lookingDirection = (GameObject.FindWithTag("Player").transform.position - position);
                Quaternion rotation = Quaternion.LookRotation(lookingDirection, Vector3.up);
                rotation.Set(0, rotation.y, 0, rotation.w);    // flatten the x and z rotation out

                //Debug.Log($"Spawning: {enemy.EnemyPrefab} with probability of {enemy.probabilityOfSpawn}% from random number ({randomEnemy})\nWith position {position} and rotation {rotation}");
                GameObject newEnemy = Instantiate(enemy.spawnPrefab, position, rotation, this.transform);
                newEnemy.name = enemy.spawnPrefab.name + " " + enemyCount;
                enemyCount++;
                //newEnemy.GetComponent<Flying>().SetDesSpeed(GameObject.FindWithTag("PilotStation").GetComponent<Flying>().desiredForwardSpeed + initialSpeedIncrease);

                break;
            }
        }
        // if rand prob not high enough, do not spawn anyone
    }

    private Vector2 CalculateRandomSpawnLocation()
    {
        float angle = Random.Range(0, 360);
        //float xPos = WorldCenter.x + WorldLength * Mathf.Cos(angle);
        //float zPos = WorldCenter.y + WorldLength * Mathf.Sin(angle);

        Transform playerTransfrom = GameObject.FindWithTag("Player").transform;
        float xPos = playerTransfrom.position.x + distanceToSpawn * Mathf.Cos(angle);
        float zPos = playerTransfrom.position.z + distanceToSpawn * Mathf.Sin(angle);

        return new Vector2(xPos, zPos);
    }

    // does not 100% work 😅
    private void OnValidate()
    {
        //if (initialSpeedIncrease <= 0) initialSpeedIncrease = 1;

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

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Transform playerTransfrom = GameObject.FindWithTag("Player").transform;
        float distance = distanceToSpawn;
        Vector3 debugPos = new Vector3(playerTransfrom.position.x, playerTransfrom.position.y, playerTransfrom.position.z);
        try
        {
            UnityEditor.Handles.color = Color.magenta;
            UnityEditor.Handles.DrawWireDisc(debugPos, this.transform.up, distance);
        }
        catch
        {

        }

#endif
    }

}