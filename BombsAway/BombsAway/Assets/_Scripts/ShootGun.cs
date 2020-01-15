using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public float gunDamage;
    public Transform bulletOrganizationTransform;

    // Start is called before the first frame update
    void Awake()
    {
        bulletOrganizationTransform = GameObject.FindGameObjectWithTag("BulletTransformStorage").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireGun()
    {
        GameObject newProjectile = Instantiate(projectile, this.transform.position + this.transform.forward *30, this.transform.rotation);
        newProjectile.layer = 8; // PLAYER Non-Intercolliding Projectiles
        BulletController newBC = newProjectile.GetComponent<BulletController>();
        newBC.allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        newBC.SetBulletDamage(gunDamage);
        newProjectile.GetComponent<Rigidbody>().velocity = this.GetComponentInParent<Rigidbody>().velocity + this.GetComponentInParent<Camera>().gameObject.transform.forward * projectileSpeed;
        if(bulletOrganizationTransform != null)
        {
            newProjectile.transform.SetParent(bulletOrganizationTransform);
        }
    }
}
