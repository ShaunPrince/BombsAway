using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCountUI : MonoBehaviour
{
    public TextMeshProUGUI ammoCountText;
    private PlayerGunController playerGunController;

    private int prevAmmoCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerGunController = this.GetComponentInChildren<PlayerGunController>();
        ammoCountText.text = playerGunController.AmmoCount() + "/" + playerGunController.magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGunController.AmmoCount() != prevAmmoCount)
        {
            ammoCountText.text = playerGunController.AmmoCount() + "/" + playerGunController.magazineSize;
            prevAmmoCount = playerGunController.AmmoCount();
        }
    }
}
