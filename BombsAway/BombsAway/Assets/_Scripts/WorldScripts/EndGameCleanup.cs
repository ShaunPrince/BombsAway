using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCleanup : MonoBehaviour
{
    private PlayerDamageEntity player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPlane").GetComponent<PlayerDamageEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health <= 0)
        {
            MissileCleanUp();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void MissileCleanUp()
    {
        // find all bombs and explode them
        GameObject[] allMissiles = GameObject.FindGameObjectsWithTag("Missile");
        foreach (GameObject missile in allMissiles)
        {
            missile.GetComponent<DamegableEnemy>().TakeDamage(10, EAllegiance.Player);
        }
    }
}
