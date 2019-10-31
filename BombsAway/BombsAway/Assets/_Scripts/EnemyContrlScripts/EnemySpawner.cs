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
    // EVENTUALLY GET THIS FROM ONE PLACE
    [Header("Don't worry about the circle, it's only in the editor")]
    public float worldCenterX;
    public float worldCenterZ;
    [Header("Radius of the world (if square, x-axis)")]
    public float worldLength;

    public int timeBetweenSpawn;
    public int minSpawnTime;
    [Range(0,1)]
    public int spawnTimeDecrement;
    [Tooltip("Increase in speed that allows the enemy to catch up initially, >0")]
    public float initialSpeedIncrease;
    public EnemySpawn[] Enemies;

    private float deltaTime = 100f;     // to create insta spawn
    private float totalWeightedProb = 0;

    // Start is called before the first frame update
    void Start()
    {
        // calculate the weighted prob
        foreach (EnemySpawn enemy in Enemies)
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
        foreach (EnemySpawn enemy in Enemies)
        {
            // spawn that enemy if it is the proper weight
            if (enemy.ProbWithinRange(randomEnemy))
            {
                // get altitude of player and spawn enemy around that altitude
                float playerYaltitude = GameObject.FindWithTag("Player").transform.position.y;

                // get random location off the map (radially)
                // calculate circumfrence of the map, choose any point along it
                System.Tuple<float, float> xzPos = CalculateRandomSpawnLocation();

                Vector3 position = new Vector3(xzPos.Item1, playerYaltitude, xzPos.Item2);

                // find direction of player and point that way
                Vector3 lookingDirection = (GameObject.FindWithTag("Player").transform.position - position);
                Quaternion rotation = Quaternion.LookRotation(lookingDirection, Vector3.up);
                rotation.Set(0, rotation.y, 0, rotation.w);    // flatten the x and z rotation out

                //Debug.Log($"Spawning: {enemy.EnemyPrefab} with probability of {enemy.probabilityOfSpawn}% from random number ({randomEnemy})\nWith position {position} and rotation {rotation}");
                GameObject newEnemy = Instantiate(enemy.EnemyPrefab, position, rotation, this.transform);
                newEnemy.GetComponent<Flying>().SetDesSpeed(GameObject.FindWithTag("PilotStation").GetComponent<Flying>().desiredForwardSpeed + initialSpeedIncrease);

                break;
            }
        }
        // if rand prob not high enough, do not spawn anyone
    }

    private System.Tuple<float, float> CalculateRandomSpawnLocation()
    {
        float angle = Random.Range(0, 360);
        float xPos = worldCenterX + worldLength * Mathf.Cos(angle);
        float zPoz = worldCenterZ + worldLength * Mathf.Sin(angle);

        return new System.Tuple<float, float>(xPos, zPoz);
    }

    // does not 100% work 😅
    private void OnValidate()
    {
        if (initialSpeedIncrease <= 0) initialSpeedIncrease = 1;

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
        // draw spawn radius
        Vector3 debugPos = new Vector3(worldCenterX, 100, worldCenterZ);
        UnityEditor.Handles.DrawWireDisc(debugPos, this.transform.up, worldLength);
#endif
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
