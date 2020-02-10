using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DebugWindow : EditorWindow
{
    bool playerInvincible = false;
    string objectName = "";
    int objectID = 1;
    GameObject objectToSpawn;
    float spawnAlt = 0;
    float spawnRadius = 2500;
    Vector3 spawnLocation;

    [MenuItem("BombsAway/DebugTools")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DebugWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Player Info / Options", EditorStyles.boldLabel);

        playerInvincible = EditorGUILayout.Toggle("Player Invincible", playerInvincible);

        if (GUILayout.Button("Apply"))
        {
            ApplyPlayerOptions();
        }

        GUILayout.Label("Spawn Objects", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Custom Name (Blank For Default)", objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to spawn", objectToSpawn, typeof(GameObject),false) as GameObject;
        spawnLocation = EditorGUILayout.Vector3Field("Coords To Spawn (leave 0 and set radius and alt for random", spawnLocation);
        spawnRadius = EditorGUILayout.FloatField("Radius From Center", spawnRadius);
        spawnAlt = EditorGUILayout.FloatField("Alt", spawnAlt);

        if(GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if(objectToSpawn != null)
        {
            Vector3 finalSpawnPos = new Vector3(0, 0, 0);
            if(spawnLocation == Vector3.zero)
            {
                Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
                finalSpawnPos = new Vector3(spawnCircle.x, spawnAlt, spawnCircle.y);
            }
            else
            {
                finalSpawnPos = spawnLocation;
            }

            GameObject newGameObject = GameObject.Instantiate(objectToSpawn, finalSpawnPos, Quaternion.identity);

            if(objectName.Length != 0)
            {
                newGameObject.name = objectName + objectID;
            }
            else
            {

                newGameObject.name = objectToSpawn.name + objectID;
            }


            ++objectID;
        }
    }

    private void ApplyPlayerOptions()
    {
        GameObject.FindObjectOfType<PlayerDamageEntity>().isInvincible = playerInvincible;
    }


}
