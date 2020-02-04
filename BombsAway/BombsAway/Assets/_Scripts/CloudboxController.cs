using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudboxController : MonoBehaviour
{
    private Transform player;
    private float[] densities = { 0.1f, 0.5f, 1f };

    private float densityTime = 5f;
    private EAlts prevAlt;

    private float prevDensity;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting != prevAlt)
        {
            UpdateCloudBox((int)player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting);
            prevAlt = player.GetComponentInChildren<PlayerFlightControls>().currentAltSetting;
        }
    }

    private void UpdateCloudBox(int index)
    {
        iTween.ValueTo(gameObject, iTween.Hash( "from", prevDensity, "to", densities[index],
                                                "time", densityTime, "easetype", "linear",
                                                "onupdate", "UpdateDensity"));
    }

    private void UpdateDensity(float density)
    {
        prevDensity = density;
        this.transform.GetComponent<Renderer>().material.SetFloat("Vector1_4FEBF9CE", density);
    }
}
