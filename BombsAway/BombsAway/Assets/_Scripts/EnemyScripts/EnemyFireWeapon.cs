using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWeapon : ShootGun
{
    private int lengthOfProjectile = 12;
    private AudioSource gunShot;

    private void Start()
    {
        gunShot = this.GetComponent<AudioSource>();
    }

    public void FireGun()
    {
        GameObject newProjectile = Instantiate(projectile, this.transform.position + this.transform.forward, this.transform.rotation);
        newProjectile.layer = 17; // ENEMY Non-Intercolliding Projectiles
        newProjectile.GetComponent<BulletController>().allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        newProjectile.GetComponent<Rigidbody>().velocity = this.GetComponentInParent<Rigidbody>().velocity + this.gameObject.transform.forward * projectileSpeed;
        gunShot.PlayOneShot(gunShot.clip);
    }

    public void FireMissile()
    {
        GameObject newProjectile = Instantiate(projectile, this.transform.position + this.transform.forward, this.transform.rotation);
        newProjectile.layer = 18; // ENEMY Non_Intercolliding Missiles
        newProjectile.GetComponent<MissileContorller>().allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        //newProjectile.GetComponent<Rigidbody>().velocity = this.GetComponentInParent<Rigidbody>().velocity + this.gameObject.transform.forward * projectileSpeed;
        //newProjectile.GetComponent<Flying>().SetDesSpeed(projectileSpeed);
        newProjectile.GetComponent<MissileContorller>().SetSpeed(projectileSpeed);
    }
}
