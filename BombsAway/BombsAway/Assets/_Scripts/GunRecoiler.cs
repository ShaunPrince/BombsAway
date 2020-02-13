using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoiler : MonoBehaviour
{
    private Transform gunModel;
    private float restingZPos;
    private float recoilDisplacement;
    private float recoilTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        gunModel = this.transform.GetChild(0);
        restingZPos = gunModel.localPosition.z;
        recoilDisplacement = 1.0f;
        recoilTime = 0.05f;
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < recoilTime)
        {
            //Debug.Log("Timer started");
            timer += Time.deltaTime;
            if (timer >= recoilTime)
            {
                gunModel.localPosition = new Vector3(gunModel.localPosition.x, gunModel.localPosition.y, restingZPos);
                //Debug.Log("Timer ended");
            }
        }
    }

    public void RecoilGun()
    {
        timer = 0.0f;
        gunModel.localPosition = new Vector3(gunModel.localPosition.x, gunModel.localPosition.y, restingZPos - recoilDisplacement);
    }
}
