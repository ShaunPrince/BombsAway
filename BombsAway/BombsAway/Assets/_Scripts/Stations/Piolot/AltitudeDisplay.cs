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
    public float normalizingNum;

    private float prevAlt;

    private PlayerFlightControls playerFlightControls;
    private Flying playerFlyingComponent;
    private EAlts prevAltitude;

    private float maxAlt;
    private float minAlt;

    private float heightDifference;

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
        prevAlt = normalizingNum;

        // set current alt on meter
        SetBulb(prevAltitude);
        // altSlider.value = playerFlightControls.GetDynamicAlt();

        maxAlt = playerFlightControls.presetAlts[playerFlightControls.presetAlts.Length - 1];
        minAlt = playerFlightControls.presetAlts[0];
        heightDifference = Mathf.Abs( Mathf.Abs(maxAlt) - Mathf.Abs(minAlt) );
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

        // the altitude fluctuates too much, check within a range
        if (playerFlyingComponent.desireAltitude - 2 > playerFlightControls.GetDynamicAlt() || playerFlightControls.GetDynamicAlt() > playerFlyingComponent.desireAltitude + 2)
        {
            SetAltitudeLiquid();
        }
    }

    private void SetBulb(EAlts index)
    {
        altitudeBulbs[(int)prevAltitude].GetComponent<MeshRenderer>().material = bulbColors[0];
        // lerp, do a little flicker
        altitudeBulbs[(int)index].GetComponent<MeshRenderer>().material = bulbColors[1];
    }

    private void SetAltitudeLiquid()
    {
        float changedAmount = NormalizeHeight(playerFlightControls.GetDynamicAlt());
        //iTween.ValueTo(gameObject, iTween.Hash("from", prevAlt, "to", changedAmount,
        //                                        "time", .2f, "easetype", "linear",
        //                                        "onupdate", "ChangeAltHeight"));
        ChangeAltHeight(changedAmount);
    }

    private float NormalizeHeight(float num)
    {
        float subtractedValue = Mathf.Abs( Mathf.Abs(maxAlt) - Mathf.Abs(num) );
        // normalize the health between -normalizingNum and normalizingNum
        float percentMaxVal = subtractedValue / heightDifference;
        float flippedPercentage = 1 - percentMaxVal;
        float precentBetween02 = flippedPercentage * (normalizingNum * 2);
        return precentBetween02 - normalizingNum;
    }

    private void ChangeAltHeight(float amount)
    {
        altitudeHightMeter.GetComponent<Renderer>().material.SetFloat("Vector1_59743A23", amount);
        prevAlt = amount;
    }
}
