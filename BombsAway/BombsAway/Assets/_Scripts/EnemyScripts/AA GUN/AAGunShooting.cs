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

    private int bulletsFiredThisBurst;

    public EnemyFireWeapon gunFireWeapon;





    // Start is called before the first frame update
    void Awake()
    {
        timeSinceLastBurst = 0f;
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

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draw spawn radius
        float gunRadius = GameObject.Find("Buidling Spawner").GetComponent<SpawnAAGuns>().gunRadius;
        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y + 300, this.transform.position.z);
        try
        {
            UnityEditor.Handles.DrawWireDisc(position, this.transform.up, gunRadius);
        }
        catch
        {

        }
#endif
    }
}
