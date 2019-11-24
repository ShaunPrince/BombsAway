using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWeapon
{
    public EPosition gunPosition;
    public GameObject gun;
    private int ammo;
    private float timeReloading = 0.0f;
    private bool reloading = false;

    public void SetAmmo(int ammoCount)
    {
        ammo = ammoCount;
    }
    public void DecreaseAmmo()
    {
        --ammo;
    }
    public void ReloadAmmo(int reload)
    {
        ammo = reload;
    }
    public bool HasAmmo()
    {
        if (ammo <= 0) return false;
        else return true;
    }
    public bool isReloading()
    {
        return reloading;
    }
    public void SetReloading(bool value)
    {
        reloading = value;
    }
    public float TimeSpentReloading() {
        return timeReloading;
    }
    public void UpdateReload(float reloadAddition) {
        timeReloading += reloadAddition;
    }
    public void ResetReload() {
        timeReloading = 0f;
    }
}

