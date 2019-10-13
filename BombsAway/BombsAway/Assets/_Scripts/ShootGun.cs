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
        GameObject newProjectile = Instantiate(projectile, this.transform.position, this.transform.rotation);
        newProjectile.GetComponent<Rigidbody>().velocity = newProjectile.transform.forward * projectileSpeed;
    }
}
