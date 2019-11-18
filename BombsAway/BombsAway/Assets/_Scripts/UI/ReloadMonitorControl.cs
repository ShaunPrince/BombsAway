using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class ReloadMonitorControl : MonoBehaviour
{
    public GameObject gunner;
    public GameObject MonitorGraphic;


    private float timer;
    private bool isReloading = false;
    private bool ReloadFlag = true;

    void Awake()
    {
        timer = gunner.GetComponent<PlayerGunController>().timeToReload;
        ReloadFlag = true;
    }


    private void SetMonitorReloadFalse() {
        MonitorGraphic.SetActive(false);
        ReloadFlag = true;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ReloadFlag) { 
            isReloading = gunner.GetComponent<PlayerGunController>().reloading;
            //Debug.Log(isReloading);
            if (isReloading) {

                MonitorGraphic.SetActive(true);
                ReloadFlag = false;
                isReloading = false;
                Invoke("SetMonitorReloadFalse", 5);
            }

        }
    }
}
