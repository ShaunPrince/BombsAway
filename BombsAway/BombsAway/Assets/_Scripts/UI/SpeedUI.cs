using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedUI : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    private Flying playerFlyingComponent;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        speedText.text = Mathf.RoundToInt(playerFlyingComponent.currentForwardSpeed) + " mph";
    }

    // Update is called once per frame
    void Update()
    {
        // the speed fluctuates too much, check within a range
        if (playerFlyingComponent.desiredForwardSpeed - 2 > playerFlyingComponent.currentForwardSpeed || playerFlyingComponent.currentForwardSpeed > playerFlyingComponent.desiredForwardSpeed + 2)
        {
            speedText.text = Mathf.RoundToInt(playerFlyingComponent.currentForwardSpeed) + " mph";
        }
    }
}
