using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunShoot : ShootGun
{
    private int lengthOfProjectile = 12;     
    public void FireGun()
    {
        GameObject newProjectile = Instantiate(projectile, this.transform.position + this.transform.forward * lengthOfProjectile, this.transform.rotation, this.transform);
        newProjectile.GetComponent<BulletController>().allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        newProjectile.GetComponent<Rigidbody>().velocity = this.GetComponentInParent<Rigidbody>().velocity + this.gameObject.transform.forward * projectileSpeed;
    }
}
