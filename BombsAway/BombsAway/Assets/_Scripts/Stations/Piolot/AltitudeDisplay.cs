using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudeDisplay : MonoBehaviour
{
    [Header("Low to high")]
    public List<GameObject> altitudeBulbs;
    [Header("Off then On")]
    public List<Material> bulbColors;

    public GameObject altitudeHightMeter;

    private PlayerFlightControls playerFlightControls;
    private Flying playerFlyingComponent;
    private EAlts prevAltitude;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        playerFlightControls = this.GetComponentInChildren<PlayerFlightControls>();

        // set max for alt meter
        // set min for alt meter
        //altSlider.maxValue = playerFlightControls.presetAlts[playerFlightControls.presetAlts.Length - 1];
        //altSlider.minValue = playerFlightControls.presetAlts[0];

        prevAltitude = playerFlightControls.currentAltSetting;

        // set current alt on meter
        SetBulb(prevAltitude);
        // altSlider.value = playerFlightControls.GetDynamicAlt();
    }

    // Update is called once per frame
    void Update()
    {
        // only change arrow when altitude first changes
        if (prevAltitude != playerFlightControls.currentAltSetting)
        {
            SetBulb(playerFlightControls.currentAltSetting);
            prevAltitude = playerFlightControls.currentAltSetting;
        }
    }

    private void SetBulb(EAlts index)
    {
        altitudeBulbs[(int)prevAltitude].GetComponent<MeshRenderer>().material = bulbColors[0];
        // lerp, do a little flicker
        altitudeBulbs[(int)index].GetComponent<MeshRenderer>().material = bulbColors[1];
    }
}
