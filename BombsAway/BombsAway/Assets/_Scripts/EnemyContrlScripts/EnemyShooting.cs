﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : DamageableEntity
{
    public float aimingDistance;
    public float timeBetweenShots;
    public float timeToReload;
    public int magazineSize;

    public EnemyGun[] enemyGuns;
    private Position gunToShoot;

    private float timeSinceShot = 0.0f;
    private float timeReloading = 0.0f;
    private int ammoCount;
    //private bool reloading = false;

    public bool playerWithinRange = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        // always check if the player is within range
        CheckIfPlayerWithinRange();
        if (gunToShoot != null && !enemyGuns[GetGunIndexFromPosition()].HasAmmo())
        {
            //reloading = true;
            enemyGuns[GetGunIndexFromPosition()].SetReloading(true);
            ReloadGun();
        }
        else if (playerWithinRange && !enemyGuns[GetGunIndexFromPosition()].isReloading())
        {
            if (timeSinceShot >= timeBetweenShots)
            {
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
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();  //CHANGE
        if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) <= aimingDistance &&
            Mathf.Abs(playerTransform.position.z - this.transform.position.z) <= aimingDistance)
        {
            if (this.GetComponent<EnemyFlying>().IsParallel() && !this.GetComponent<EnemyFlying>().IsDodging()) {
                playerWithinRange = true;
                CheckWhichPositionPlayerIs(playerTransform);
            }
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

        // slope of player-enemy
        float slopePE = (playerTransform.position.z - this.transform.position.z) / (playerTransform.position.x - this.transform.position.x);
        float slopeEE = this.transform.forward.x == 0 ? 0f : (this.transform.forward.z) / (this.transform.forward.x);
        float angle = Vector3.SignedAngle(this.transform.forward, new Vector3(playerTransform.position.x - this.transform.position.x, 0, playerTransform.position.z - this.transform.position.z) , Vector3.up);
        //Debug.Log($"Angle: {angle}, {this.transform.forward}");

        // to the left of the enemy
        if (angle <= 0)
        {
            gunToShoot = Position.Left;
        }
        // in front of the enemy
        // to the right of the enemy
        else if (angle > 0)
        {
            gunToShoot = Position.Right;
        }
        //Debug.Log($"Player is to the {gunToShoot} of enemy");
        // behind the enemy
    }

    private void AimGunAtPlayer()
    {
        int gunIndex = GetGunIndexFromPosition();
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();   // MAKE MORE EFFICIENT
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - enemyGuns[gunIndex].gun.transform.position, Vector3.up);
        enemyGuns[gunIndex].gun.transform.rotation = rotation;
    }

    private void Shoot()
    {
        // do a shoot
        int gunIndex = GetGunIndexFromPosition();
        //ammoCount--;
        enemyGuns[gunIndex].DecreaseAmmo();
        //Debug.Log($"Enemy shooting {enemyGuns[gunIndex].gunPosition} gun!");
        enemyGuns[gunIndex].gun.GetComponent<EnemyGunShoot>().FireGun();
}

    private void ReloadGun()
    {
        if (timeReloading >= timeToReload)
        {
            //reloading = false;
            //ammoCount = magazineSize;
            int gunIndex = GetGunIndexFromPosition();
            enemyGuns[gunIndex].ReloadAmmo(magazineSize);
            enemyGuns[gunIndex].SetReloading(false);
            timeReloading = 0.0f;
            //Debug.Log($"Enemy reloaded and ready to re-enter fight");
        }
        else
        {
            timeReloading += Time.deltaTime;
        }
    }

    private int GetGunIndexFromPosition() {
        for (int i = 0; i < enemyGuns.Length; ++i) {
            if (enemyGuns[i].gunPosition == gunToShoot) {
                return i;
            }
        }
        return -1;  // something went wrong
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draw view area of enemy
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, aimingDistance);
#endif
    }
}

[System.Serializable]
public class EnemyGun
{
    public Position gunPosition;
    public GameObject gun;
    private int ammo;
    private bool reloading = false;

    public void DecreaseAmmo()
    {
        --ammo;
    }
    public void ReloadAmmo(int reload)
    {
        ammo = reload;
    }
    public bool HasAmmo()
    {
        if (ammo <= 0) return false;
        else return true;
    }
    public bool isReloading()
    {
        return reloading;
    }
    public void SetReloading(bool value)
    {
        reloading = value;
    }
}
