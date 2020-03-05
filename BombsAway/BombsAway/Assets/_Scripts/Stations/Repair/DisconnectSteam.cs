using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectSteam : MonoBehaviour
{
    public GameObject steamRelease;
    private bool prevConnected = false;
    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<TankController>())
        {
            prevConnected = this.GetComponent<TankController>().IsConnectedToAtLeastOneJunction();
        }
        else if (this.GetComponent<SourceJunction>())
        {
            prevConnected =  this.GetComponent<SourceJunction>().IsConnectedToAtLeastOneJunction();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TankController>())
        {
            SteamPoof(this.GetComponent<TankController>().IsConnectedToAtLeastOneJunction());
        }
        else if (this.GetComponent<SourceJunction>())
        {
            SteamPoof(this.GetComponent<SourceJunction>().IsConnectedToAtLeastOneJunction());
        }
    }

    private void SteamPoof(bool currentStatus)
    {
        if (currentStatus != prevConnected)
        {
            if (prevConnected == true)
            {
                steamRelease.GetComponent<ParticleSystem>().Play();
            }

            prevConnected = currentStatus;
        }
    }
}
