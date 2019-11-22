using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class ReloadMonitorControl : MonoBehaviour
{
    public GameObject gunner;
    public GameObject MonitorGraphic;


    private ReloadManager rm;
    private float timer;
    private bool isReloading = false;
    private bool ReloadFlag = true;

    void Awake()
    {
        rm = this.gameObject.GetComponentInParent<ReloadManager>();
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
            //isReloading = false;
            isReloading = rm.getReloadingStatus();
            //Debug.Log("Reload moniter is reloading: " + isReloading);
            if (isReloading) {
                
                MonitorGraphic.SetActive(true);
                ReloadFlag = false;
                isReloading = false;
                Invoke("SetMonitorReloadFalse", 5.1f);
            }

        }
    }
}
