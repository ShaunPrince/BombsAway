using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class AmmoCountUI : MonoBehaviour
{
    //public TextMeshProUGUI ammoCountText;
    public PhysicalDoubleDigits counterDigits;
    public PhysicalDoubleDigits maxDigits;
    private PlayerGunController playerGunController;

    private int prevAmmoCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerGunController = this.GetComponentInChildren<PlayerGunController>();
        //ammoCountText.text = playerGunController.AmmoCount() + "/" + playerGunController.magazineSize;
        counterDigits.SetDoubleNumber(playerGunController.AmmoCount());
        maxDigits.SetDoubleNumber(playerGunController.magazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGunController.AmmoCount() != prevAmmoCount)
        {
            //ammoCountText.text = playerGunController.AmmoCount() + "/" + playerGunController.magazineSize;
            counterDigits.SetDoubleNumber(playerGunController.AmmoCount());
            prevAmmoCount = playerGunController.AmmoCount();
        }
    }
}
