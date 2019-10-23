using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireGun()
    {
        GameObject newProjectile = Instantiate(projectile, this.transform.position + this.transform.forward *10, this.transform.rotation, this.transform);
        newProjectile.GetComponent<BulletController>().allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        newProjectile.GetComponent<Rigidbody>().velocity = this.GetComponentInParent<Rigidbody>().velocity + this.GetComponentInParent<Camera>().gameObject.transform.forward * projectileSpeed;
    }
}
