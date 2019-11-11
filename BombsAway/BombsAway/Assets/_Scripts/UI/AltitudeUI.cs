using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltitudeUI : MonoBehaviour
{
    public Slider altSlider;
    public GameObject altitudeTextPrefab;
    public Image altitudeArrow;
    private PlayerFlightControls playerFlightControls;
    private Flying playerFlyingComponent;
    private Vector3[] sliderPresets;
    private EAlts prevAltitude;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        playerFlightControls = this.GetComponentInChildren<PlayerFlightControls>();
        altSlider.maxValue = playerFlightControls.presetAlts[playerFlightControls.presetAlts.Length - 1];
        altSlider.minValue = playerFlightControls.presetAlts[0];
        //altText.text = Mathf.RoundToInt(playerFlyingComponent.currentAltitude) + "m";
        sliderPresets = new Vector3[playerFlightControls.presetAlts.Length];

        // spawn the altitude labels
        for (int i = 0; i < playerFlightControls.presetAlts.Length; i++)
        {
            GameObject altitudeObject = Instantiate(altitudeTextPrefab, altSlider.transform);
            altitudeObject.GetComponent<TextMeshProUGUI>().text = ((EAlts)i).ToString();
            altitudeObject.name = ((EAlts)i).ToString() + " Altitude Text";

            float heightPercent = playerFlightControls.presetAlts[i] / altSlider.maxValue;
            float yPos = altSlider.GetComponent<RectTransform>().rect.height * heightPercent;
            Vector3 textAlt = new Vector3(70, yPos - 90, 0);
            altitudeObject.transform.localPosition = textAlt;

            //Debug.Log($"height%: {heightPercent}, yPos: {yPos}, altitudeObject: {altitudeObject.transform.position}");
            altitudeObject.SetActive(true);

            textAlt.x -= 95;
            sliderPresets[i] = textAlt;
        }

        prevAltitude = playerFlightControls.currentAltSetting;
        altitudeArrow.transform.localPosition = sliderPresets[(int)playerFlightControls.currentAltSetting];
        altSlider.value = playerFlyingComponent.currentAltitude;
    }

    // Update is called once per frame
    void Update()
    {
        // the altitude fluctuates too much, check within a range
        if (playerFlyingComponent.desireAltitude - 2 > playerFlyingComponent.currentAltitude || playerFlyingComponent.currentAltitude > playerFlyingComponent.desireAltitude + 2)
        {
            altSlider.value = playerFlyingComponent.currentAltitude;
            //altText.text = Mathf.RoundToInt(playerFlyingComponent.currentAltitude) + " m";
        }

        // only change arrow when altitude first changes
        if (prevAltitude != playerFlightControls.currentAltSetting)
        {
            altitudeArrow.transform.localPosition = sliderPresets[(int)playerFlightControls.currentAltSetting];
            prevAltitude = playerFlightControls.currentAltSetting;
        }
    }
}
