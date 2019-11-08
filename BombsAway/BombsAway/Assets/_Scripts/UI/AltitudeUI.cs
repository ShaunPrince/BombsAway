using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltitudeUI : MonoBehaviour
{
    public Slider altSlider;
    public TextMeshProUGUI altText;
    private PlayerFlightControls playerFlightControls;
    private Flying playerFlyingComponent;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        playerFlightControls = this.GetComponentInChildren<PlayerFlightControls>();
        altSlider.maxValue = playerFlightControls.presetAlts[playerFlightControls.presetAlts.Length - 1];
        altSlider.minValue = playerFlightControls.presetAlts[0];
        altSlider.value = playerFlyingComponent.currentAltitude;
        altText.text = Mathf.RoundToInt(playerFlyingComponent.currentAltitude) + "m";
    }

    // Update is called once per frame
    void Update()
    {
        // the altitude fluctuates too much, check within a range
        if (playerFlyingComponent.desireAltitude - 2 > playerFlyingComponent.currentAltitude || playerFlyingComponent.currentAltitude > playerFlyingComponent.desireAltitude + 2)
        {
            altSlider.value = playerFlyingComponent.currentAltitude;
            altText.text = Mathf.RoundToInt(playerFlyingComponent.currentAltitude) + " m";
        }
    }
}
