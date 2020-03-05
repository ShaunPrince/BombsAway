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
        if (player.GetComponent<PlayerVictory>().HasGameEnded())
        {
            MissileCleanUp();
            MouseUnlock();
        }
    }

    private void MouseUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
