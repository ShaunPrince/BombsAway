using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    public float timeBetweenShots;
    public float timeToReload;
    public int magazineSize;
    //public GameObject GunShotCtr;
    private AudioSource GunShot;

    private ShootGun sg;
    //private GunnerUIController guic;
    private float timeSinceShot;
    private int ammoCount;
    private float timeReloading;
    public bool reloading;
    // Start is called before the first frame update
    void Start()
    {
        GunShot = this.GetComponent<AudioSource>();
        sg = this.GetComponent<ShootGun>();
        //guic = this.GetComponent<GunnerUIController>();
        timeSinceShot = 0.0f;
        ammoCount = magazineSize;
        timeReloading = 0.0f;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            ReloadingGun();
        }
        else if ((Input.GetKeyDown(KeyCode.R) && ammoCount < magazineSize && !reloading) || (ammoCount <= 0 && !reloading))
        {
            ReloadGun();
        }
        else
        {
            if (Input.GetMouseButton(0) && timeSinceShot >= timeBetweenShots)
            {
                GunShot.Play();
                sg.FireGun();
                ammoCount--;
                //guic.UpdateAmmoCount(ammoCount);
                timeSinceShot = 0.0f;
            }
            else if (Input.GetMouseButton(0) && timeSinceShot < timeBetweenShots)
            {
                timeSinceShot += Time.deltaTime;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                timeSinceShot = timeBetweenShots;
            }
        }
    }

    public int AmmoCount()
    {
        return ammoCount;
    }

    private void ReloadGun()
    {
        reloading = true;
    }

    private void ReloadingGun()
    {
        if (timeReloading >= timeToReload)
        {
            reloading = false;
            ammoCount = magazineSize;
            //guic.UpdateAmmoCount(ammoCount);
            timeReloading = 0.0f;
        }
        else
        {
            timeReloading += Time.deltaTime;
            //guic.Reloading(ammoCount);
        }
    }
}
