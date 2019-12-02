using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootGun : MonoBehaviour
{
    [Header("Guns")]
    //public float aimingDistance;
    public float timeBetweenShots;
    public float timeToReload;
    public int magazineSize;

    public EnemyWeapon[] enemyGuns;
    private EPosition gunToShoot;

    private float timeSinceShot = 0.0f;
    //private float timeReloading = 0.0f;
    //private int ammoCount;
    //private bool reloading = false;

    private bool playerWithinRange = false;

    public EPosition CurrentGunBeingShot() {
        return gunToShoot;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyGuns.Length; i++)
        {
            enemyGuns[i].SetAmmo(magazineSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // always check if the player is within range
        CheckIfPlayerWithinRange();
        for (int i = 0; i < enemyGuns.Length; i++) { 
            if (!enemyGuns[i].HasAmmo()) {
                enemyGuns[i].SetReloading(true);
            }
            if (enemyGuns[i].isReloading()) {
                ReloadGun(i);
            }
        }
        if (playerWithinRange && !enemyGuns[GetGunIndexFromPosition()].isReloading())
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
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();  //CHANGE
        //if (Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= aimingDistance)
        //{
            if (this.GetComponent<EnemyFlying>().IsParallel() && !this.GetComponent<EnemyFlying>().IsDodging()) {
                playerWithinRange = true;
                CheckWhichPositionPlayerIs(playerTransform);
            }
        //}
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
    }

    private void AimGunAtPlayer()
    {
        int gunIndex = GetGunIndexFromPosition();
        Transform playerTransform = this.GetComponent<EnemyFlying>().GetPlayerPosition();   // MAKE MORE EFFICIENT
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - enemyGuns[gunIndex].gun.transform.position, Vector3.up);
        // FIX, MAKE SMOOTH MOVEMENT
        //enemyGuns[gunIndex].gun.transform.rotation = Quaternion.Lerp(enemyGuns[gunIndex].gun.transform.rotation, rotation, Time.time);
        enemyGuns[gunIndex].gun.transform.rotation = rotation;
    }

    private void Shoot()
    {
        // do a shoot
        int gunIndex = GetGunIndexFromPosition();
        enemyGuns[gunIndex].DecreaseAmmo();
        //Debug.Log($"Enemy shooting {enemyGuns[gunIndex].gunPosition} gun!");
        enemyGuns[gunIndex].gun.GetComponent<EnemyFireWeapon>().FireGun();
}

    private void ReloadGun(int gunIndex)
    {
        if (enemyGuns[gunIndex].TimeSpentReloading() >= timeToReload)
        {
            enemyGuns[gunIndex].ReloadAmmo(magazineSize);
            enemyGuns[gunIndex].SetReloading(false);
            enemyGuns[gunIndex].ResetReload();
            //Debug.Log($"Enemy reloaded and ready to re-enter fight");
        }
        else
        {
            enemyGuns[gunIndex].UpdateReload(Time.deltaTime);
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
        UnityEditor.Handles.color = Color.cyan;
        // draw each gun view
        for (int i = 0; i < enemyGuns.Length; i++)
        {
            try{
                UnityEditor.Handles.DrawLine(enemyGuns[i].gun.transform.position, enemyGuns[i].gun.transform.position + enemyGuns[i].gun.transform.forward * 700);
            }
            catch
            {

            }
        }
#endif
    }
}