using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedUI : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public Slider speedSlider;
    private Flying playerFlyingComponent;
    private PlayerFlightControls playerFlightControls;
    private ESpeeds prevSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        playerFlightControls = this.GetComponentInChildren<PlayerFlightControls>();

        speedSlider.maxValue = playerFlightControls.presetSpeeds[playerFlightControls.presetSpeeds.Length - 1];
        speedSlider.minValue = playerFlightControls.presetSpeeds[0];
        speedSlider.value = playerFlyingComponent.currentForwardSpeed;

        speedText.text = playerFlightControls.currentSpeedSetting.ToString(); //Mathf.RoundToInt(playerFlyingComponent.currentForwardSpeed) + " mph";
        prevSpeed = playerFlightControls.currentSpeedSetting;
    }

    // Update is called once per frame
    void Update()
    {

        // the speed fluctuates too much, check within a range
        if (playerFlyingComponent.desiredForwardSpeed - 2 > playerFlyingComponent.currentForwardSpeed || playerFlyingComponent.currentForwardSpeed > playerFlyingComponent.desiredForwardSpeed + 2)
        {
            speedSlider.value = playerFlyingComponent.currentForwardSpeed;
        }

        if (prevSpeed != playerFlightControls.currentSpeedSetting)
        {
            speedText.text = playerFlightControls.currentSpeedSetting.ToString();
            prevSpeed = playerFlightControls.currentSpeedSetting;
        }
    }
}
