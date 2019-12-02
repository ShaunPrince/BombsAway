using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootMissile : MonoBehaviour
{
    [Header("Missiles")]
    public float timeBetweenShots;
    public float timeToReload;
    public int magazineSize;

    public EnemyWeapon[] enemyMissiles;
    private EPosition gunToShoot;

    private float timeSinceShot = 0.0f;

    private bool playerWithinRange = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyMissiles.Length; i++)
        {
            enemyMissiles[i].SetAmmo(magazineSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // always check if the player is within range
        CheckIfPlayerWithinRange();
        for (int i = 0; i < enemyMissiles.Length; i++)
        {
            if (!enemyMissiles[i].HasAmmo())
            {
                enemyMissiles[i].SetReloading(true);
            }
            if (enemyMissiles[i].isReloading())
            {
                ReloadGun(i);
            }
        }
        if (playerWithinRange && !enemyMissiles[GetGunIndexFromPosition()].isReloading())
        {
            if (timeSinceShot >= timeBetweenShots)
            {
                //Debug.Log($"Aiming and shooting");
                AimGunAtPlayer();
                Shoot();
                timeSinceShot = 0.0f;
            }
            else
            {
                timeSinceShot += Time.deltaTime;
            }
        }
    }

    private void CheckIfPlayerWithinRange()
    {
        // only shoot missiles if farther away (not doding, or parallel
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();  //CHANGE
        if (this.GetComponent<EnemyFlying>().IsWithinVisionRange() && 
            !this.GetComponent<EnemyFlying>().IsDodging() && 
            !this.GetComponent<EnemyFlying>().IsParallel())
        {
            playerWithinRange = true;
            CheckWhichPositionPlayerIs(playerTransform);
        }
        else
        {
            playerWithinRange = false;
        }
    }

    private void CheckWhichPositionPlayerIs(Transform playerTransform)
    {
        // find the line from the center of the player to the center of the enemy
        // angle relative to player.transform.forward
        // 0-180 right, 180-360 left
        /*
        // slope of player-enemy
        float slopePE = (playerTransform.position.z - this.transform.position.z) / (playerTransform.position.x - this.transform.position.x);
        float slopeEE = this.transform.forward.x == 0 ? 0f : (this.transform.forward.z) / (this.transform.forward.x);
        float angle = Vector3.SignedAngle(this.transform.forward, new Vector3(playerTransform.position.x - this.transform.position.x, 0, playerTransform.position.z - this.transform.position.z), Vector3.up);
        //Debug.Log($"Angle: {angle}, {this.transform.forward}");

        // to the left of the enemy
        if (angle <= 0)
        {
            gunToShoot = EPosition.Left;
        }
        // in front of the enemy
        // to the right of the enemy
        else if (angle > 0)
        {
            gunToShoot = EPosition.Right;
        }
        //Debug.Log($"Player is to the {gunToShoot} of enemy");
        // behind the enemy
        */

        // Right now there are only forward missiles
        gunToShoot = EPosition.Front;
    }

    private void AimGunAtPlayer()
    {
        int gunIndex = GetGunIndexFromPosition();
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();   // MAKE MORE EFFICIENT
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - enemyMissiles[gunIndex].gun.transform.position, Vector3.up);
        // FIX, MAKE SMOOTH MOVEMENT
        //enemyGuns[gunIndex].gun.transform.rotation = Quaternion.Lerp(enemyGuns[gunIndex].gun.transform.rotation, rotation, Time.time);
        enemyMissiles[gunIndex].gun.transform.rotation = rotation;
    }

    private void Shoot()
    {
        // do a shoot
        int gunIndex = GetGunIndexFromPosition();
        enemyMissiles[gunIndex].DecreaseAmmo();
        //Debug.Log($"Enemy shooting {enemyMissiles[gunIndex].gunPosition} missile!");
        enemyMissiles[gunIndex].gun.GetComponent<EnemyFireWeapon>().FireMissile();
    }

    private void ReloadGun(int gunIndex)
    {
        if (enemyMissiles[gunIndex].TimeSpentReloading() >= timeToReload)
        {
            enemyMissiles[gunIndex].ReloadAmmo(magazineSize);
            enemyMissiles[gunIndex].SetReloading(false);
            enemyMissiles[gunIndex].ResetReload();
            //Debug.Log($"Enemy reloaded and ready to re-enter fight");
        }
        else
        {
            enemyMissiles[gunIndex].UpdateReload(Time.deltaTime);
        }
    }

    private int GetGunIndexFromPosition()
    {
        for (int i = 0; i < enemyMissiles.Length; ++i)
        {
            if (enemyMissiles[i].gunPosition == gunToShoot)
            {
                return i;
            }
        }
        return -1;  // something went wrong
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.cyan;
        // draw each gun view
        for (int i = 0; i < enemyMissiles.Length; i++)
        {
            try
            {
                UnityEditor.Handles.DrawLine(enemyMissiles[i].gun.transform.position, enemyMissiles[i].gun.transform.position + enemyMissiles[i].gun.transform.forward * 700);
            }
            catch
            {

            }
        }
#endif
    }
}
