using System.Collections;
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
    private bool reloading = false;

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
        if (ammoCount <= 0 && !reloading)
        {
            reloading = true;
        }
        else if (reloading)
        {
            ReloadGun();
        }
        else if (playerWithinRange && !reloading)
        {
            if (timeSinceShot >= timeBetweenShots)
            {
                AimGunAtPlayer();
                Shoot();
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
            if (this.GetComponent<EnemyFlying>().IsParallel()) {
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
        // to the left of the enemy
        if (playerTransform.position.x - this.transform.position.x <= 0 ||
            playerTransform.position.z - this.transform.position.z <= 0)
        {
            gunToShoot = Position.Left;
        }
        // in front of the enemy
        // to the right of the enemy
        if (playerTransform.position.x - this.transform.position.x > 0 ||
            playerTransform.position.z - this.transform.position.z > 0)
        {
            gunToShoot = Position.Right;
        }
        // behind the enemy
    }

    private void AimGunAtPlayer()
    {
        int gunIndex = GetGunIndexFromPosition();
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();   // MAKE MORE EFFICIENT
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - enemyGuns[gunIndex].gun.transform.position, Vector3.up);
        //float eulerDirection = rotation.eulerAngles.y;
        enemyGuns[gunIndex].gun.transform.rotation = rotation;
    }

    private void Shoot()
    {
        // do a shoot
        int gunIndex = GetGunIndexFromPosition();
        ammoCount--;
        Debug.Log($"Enemy shooting {enemyGuns[gunIndex].gunPosition} gun! Ammo count at {ammoCount}");
}

    private void ReloadGun()
    {
        if (timeReloading >= timeToReload)
        {
            reloading = false;
            ammoCount = magazineSize;
            timeReloading = 0.0f;
            Debug.Log($"Enemy reloaded and ready to re-enter fight");
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

    public void decreaseAmmo()
    {
        --ammo;
    }
    public void reloadAmmo(int reload)
    {
        ammo = reload;
    }
    public bool hasAmmo()
    {
        if (ammo <= 0) return false;
        else return true;
    }
}
