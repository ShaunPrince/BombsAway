using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShootingScript : MonoBehaviour
{
    private EnemyGunShoot shootGun;
    // Start is called before the first frame update
    void Start()
    {
        shootGun = this.GetComponent<EnemyGunShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            shootGun.FireMissile();
        }
    }
}
