using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudboxController : MonoBehaviour
{
    private Transform player;
    private float[] densities = { 0.1f, 0.3f, .5f };
    private float[] speeds = { 0.5f, 0.8f, 1.2f };

    private float changeOverTime = 10f;
    private EAlts prevAlt;
    private ESpeeds prevSpeed;

    private float prevDensity;
    private float prevSpeedVal;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent;

        // set first density
        UpdateDensity(densities[(int)player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting]);
        prevAlt = player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting;

        // set first speed
        UpdateSpeed(speeds[(int)player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting]);
        prevSpeed = player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // change in alt
        if (player.GetChild(0).gameObject.active)
        {
            if (player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting != prevAlt)
            {
                UpdateCloudBoxDensity((int)player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting);
                prevAlt = player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting;
            }

            // change in speed
            if (player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting != prevSpeed)
            {
                // figure out how to smooth this!
                UpdateCloudBoxSpeed((int)player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting);
                //UpdateSpeed(speeds[(int)player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting]);
                prevSpeed = player.GetComponentInChildren<PlayerFlightControls>().currentSpeedSetting;
            }
        }
    }

    private void UpdateCloudBoxDensity(int index)
    {
        iTween.ValueTo(gameObject, iTween.Hash( "from", prevDensity, "to", densities[index],
                                                "time", changeOverTime, "easetype", "linear",
                                                "onupdate", "UpdateDensity"));
    }

    private void UpdateCloudBoxSpeed(int index)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", prevSpeedVal, "to", speeds[index],
                                                "time", changeOverTime, "easetype", "linear",
                                                "onupdate", "UpdateSpeed"));
    }

    private void UpdateDensity(float density)
    {
        prevDensity = density;
        this.transform.GetComponent<Renderer>().material.SetFloat("Vector1_4FEBF9CE", density);
    }

    private void UpdateSpeed(float speed)
    {
        prevSpeedVal = speed;
        this.transform.GetComponent<Renderer>().material.SetFloat("Vector1_38E055BE", speed);
    }
}
