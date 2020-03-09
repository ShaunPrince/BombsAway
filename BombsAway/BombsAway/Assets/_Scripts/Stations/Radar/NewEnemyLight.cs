using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyLight : MonoBehaviour
{
    public GameObject lightBulb;
    public Material onMaterial;
    public Material offMaterial;
    private EnemySpawner spawner;
    private int prevEnemyCount = 0;

    private bool countDown = false;
    private float timer = 0f;
    private float maxTimer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevEnemyCount < spawner.NumberOfEnemiesSpawned())
        {
            lightBulb.GetComponent<Renderer>().material = onMaterial;
            //lightBulbOn.GetComponent<MaterialTweening>().MergeMaterialGlow(offMaterial, onMaterial);
            countDown = true;
            prevEnemyCount = spawner.NumberOfEnemiesSpawned();
        }

        if (countDown)
        {
            if (timer > maxTimer)
            {
                lightBulb.GetComponent<Renderer>().material = offMaterial;
                //lightBulbOn.GetComponent<MaterialTweening>().MergeMaterialGlow(onMaterial, offMaterial);
                countDown = false;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
