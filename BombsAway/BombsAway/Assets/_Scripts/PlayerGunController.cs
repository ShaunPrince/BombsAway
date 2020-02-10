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
    //public AudioSource startFiring;
    //public AudioSource gunFiring;
    //public AudioSource endFiring;

    private ShootGun sg;
    private ReloadManager rm;
    private GunRecoiler gr;
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
        gr = this.GetComponentInParent<GunRecoiler>();
        //guic = this.GetComponent<GunnerUIController>();
        timeSinceShot = 0.0f;
        ammoCount = magazineSize;
        timeReloading = 0.0f;
        reloading = false;
        rm = this.GetComponentInParent<ReloadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.R) && ammoCount < magazineSize && !reloading) || (ammoCount <= 0 && !reloading))
        {
            rm.ReloadWeapon(timeToReload);
            reloading = true;
        }
        else if (!reloading)
        {
            if (Input.GetMouseButton(0) && timeSinceShot >= timeBetweenShots)
            {
                GunShot.Play();
                sg.FireGun();
                gr.RecoilGun();
                ammoCount--;
                //guic.UpdateAmmoCount(ammoCount);
                timeSinceShot = 0.0f;
            }
            else if (timeSinceShot < timeBetweenShots)
            {
                timeSinceShot += Time.deltaTime;
            }
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    timeSinceShot = timeBetweenShots;
            //}
        }
        else
        {
            reloading = rm.getReloadingStatus();
            if (reloading == false)
            {
                ammoCount = magazineSize;
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
