using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : DamageableEntity
{
    public float aimingDistance;
    public float timeBetweenShots;
    public float timeToReload;
    public int magazineSize;

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
        Transform playerTransform =  this.GetComponent<EnemyFlying>().GetPlayerPosition();
        if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) <= aimingDistance &&
            Mathf.Abs(playerTransform.position.z - this.transform.position.z) <= aimingDistance)
        {
            playerWithinRange = true;
        }
        else
        {
            playerWithinRange = false;
        }
    }

    private void Shoot()
    {
        // do a shoot
        ammoCount--;
        Debug.Log($"Enemy shooting! Ammo count at {ammoCount}");
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

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draw view area of enemy
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, aimingDistance);
#endif
    }
}
