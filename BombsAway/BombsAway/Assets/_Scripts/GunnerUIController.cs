using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunnerUIController : MonoBehaviour
{
    public Canvas gunUI;

    private Text ammoCounterTxt;

    // Start is called before the first frame update
    void Start()
    {
        ammoCounterTxt = gunUI.transform.GetChild(0).GetComponent<Text>();
        ammoCounterTxt.text = "Ammo: " + this.GetComponent<PlayerGunController>().magazineSize.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmoCount(int newAmmo)
    {
        ammoCounterTxt.text = "Ammo: " + newAmmo.ToString();
    }

    public void Reloading(int newAmmo)
    {
        ammoCounterTxt.text = "Ammo: " + newAmmo.ToString() + "\nReloading...";
    }
}
