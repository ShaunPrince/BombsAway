using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DebugWindow : EditorWindow
{
    bool playerInvincible = false;
    bool playerUnlimitedBombs = false;
    bool playerRapidBombReload = false;
    string objectName = "";
    int objectID = 1;
    GameObject objectToSpawn;
    float spawnAlt = 0;
    float spawnRadius = 2500;
    Vector3 spawnLocation;


    private float prevHealth;
    private int prevBombCount;
    private float prevBombReloadTime;

    private Transform playerTransform;

    [MenuItem("BombsAway/DebugTools")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DebugWindow));
    }


    private void OnGUI()
    {

        GUILayout.Label("Player Info / Options", EditorStyles.boldLabel);

        playerInvincible = EditorGUILayout.Toggle("Player Invincible", playerInvincible);
        playerUnlimitedBombs = EditorGUILayout.Toggle("Player Unlimited Bombs", playerUnlimitedBombs);
        playerRapidBombReload = EditorGUILayout.Toggle("Player Instant Bomb Reload", playerRapidBombReload);



        GUILayout.Label("Spawn Objects", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Custom Name (Blank For Default)", objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to spawn", objectToSpawn, typeof(GameObject),false) as GameObject;
        spawnLocation = EditorGUILayout.Vector3Field("Coords To Spawn From (leave 0 for players location)", spawnLocation);
        spawnRadius = EditorGUILayout.FloatField("Radius From Spawn Point", spawnRadius);
        spawnAlt = EditorGUILayout.FloatField("Alt Offset From Spawn", spawnAlt);

        if(GUILayout.Button("Spawn Object"))
        {
            SpawnObject(objectToSpawn);
        }

        if(GUILayout.Button("Spawn Basic Enemy"))
        {
            //SpawnObject(Resources.Load("Assets/Prefabs/Enemies/TestEnemy.prefab") as GameObject);
            SpawnObject(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/TestEnemy.prefab" , typeof(GameObject)) as GameObject);
        }

        GUILayout.Label("Game Options", EditorStyles.boldLabel);

        if (GUILayout.Button("Kill Player"))
        {
            KillPlayer();
        }

        if (GUILayout.Button("Win Game"))
        {
            WinGame();
        }
    }

    private void SpawnObject(GameObject templateObj)
    {
        if(templateObj != null)
        {
            playerTransform = GameObject.FindObjectOfType<PlayerDamageEntity>().transform;
            Vector3 finalSpawnPos = new Vector3(0, 0, 0);
            if(spawnLocation == Vector3.zero)
            {
                Vector2 spawnCircle = Random.insideUnitCircle.normalized * spawnRadius;
                finalSpawnPos = playerTransform.position + new Vector3(spawnCircle.x, spawnAlt, spawnCircle.y);
            }
            else
            {
                finalSpawnPos = spawnLocation;
            }

            GameObject newGameObject = GameObject.Instantiate(templateObj, finalSpawnPos, Quaternion.identity);

            if(objectName.Length != 0)
            {
                newGameObject.name = objectName + objectID;
            }
            else
            {

                newGameObject.name = templateObj.name + objectID;
            }


            ++objectID;
        }
    }

    private void Update()
    {
        ApplyPlayerOptions();
    }

    private void ApplyPlayerOptions()
    {
        HandlePlayerInvincible();
        HandlePlayerUnlimitedBombs();
        HandlePlayerInstantBombReload();

    }

    private void HandlePlayerInvincible()
    {
        PlayerDamageEntity playerDamageableEntity = GameObject.FindObjectOfType<PlayerDamageEntity>();


        if (playerDamageableEntity.health != float.PositiveInfinity)
        {
            prevHealth = playerDamageableEntity.health;
        }

        if (playerInvincible == true)
        {
            playerDamageableEntity.health = float.PositiveInfinity;
        }
        else
        {
            playerDamageableEntity.health = prevHealth;
        }
    }

    private void HandlePlayerUnlimitedBombs()
    {
        BombBayControls bombBayControls = GameObject.FindObjectOfType<BombBayControls>();


        if (bombBayControls.numOfBombs < 1000)
        {
            prevBombCount = bombBayControls.numOfBombs;
        }

        if (playerUnlimitedBombs == true)
        {
            bombBayControls.numOfBombs = 999999999;
        }
        else
        {
            bombBayControls.numOfBombs = prevBombCount;
        }
    }

    private void HandlePlayerInstantBombReload()
    {
        BombBayControls bombBayControls = GameObject.FindObjectOfType<BombBayControls>();


        if (bombBayControls.reloadTime != 0)
        {
            prevBombReloadTime = bombBayControls.reloadTime;
        }

        if (playerRapidBombReload == true)
        {
            bombBayControls.reloadTime = 0;
        }
        else
        {
            bombBayControls.reloadTime = prevBombReloadTime;
        }
    }

    private void KillPlayer()
    {
        PlayerDamageEntity playerDamageableEntity = GameObject.FindObjectOfType<PlayerDamageEntity>();


        playerDamageableEntity.health = 0;
    }

    private void WinGame()
    {
        MissionManager.numberRemainingTargets = 0;
    }

}
