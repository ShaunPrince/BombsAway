using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunShooting : MonoBehaviour
{
    public int bulletsPerBurst;

    public float timeBetweenBurst;
    private float timeSinceLastBurst;

    public float timeBetweenShots;
    private float timeSinceLastShot;

    public float timeBetweenMissles;
    private float timeSinceLastMissle;

    private int bulletsFiredThisBurst;

    public EnemyFireWeapon gunFireWeapon;
    public EnemyFireWeapon missleFireWeapon;




    // Start is called before the first frame update
    void Awake()
    {
        timeSinceLastBurst = 0f;
        timeSinceLastMissle = 0f;
        timeSinceLastShot = 0f;
        bulletsFiredThisBurst = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShootingTimes();
    }

    private void UpdateShootingTimes()
    {
        timeSinceLastBurst += Time.deltaTime;
        timeSinceLastMissle += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;
    }

    public void ShootBurst()
    {
        if(timeSinceLastBurst >= timeBetweenBurst)
        {

            if(timeSinceLastShot >= timeBetweenShots)
            { 
                if(bulletsFiredThisBurst < bulletsPerBurst)
                {
                    gunFireWeapon.FireGun();
                    bulletsFiredThisBurst += 1;
                    timeSinceLastShot = 0f;
                }
                else
                {
                    timeSinceLastBurst = 0f;
                    bulletsFiredThisBurst = 0;
                }
            }
        }
        
    }

    public void ShootMissle()
    {
        if (timeSinceLastMissle >= timeBetweenMissles)
        {
            timeSinceLastMissle= 0;
            missleFireWeapon.FireMissile();

        }
    }
}
