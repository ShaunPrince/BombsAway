using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{
    public GameObject speedPointer;
    [Header("Slow to fast")]
    private float[] rotations = { 76f, 0f, -76f };

    private Flying playerFlyingComponent;
    private PlayerFlightControls playerFlightControls;
    private ESpeeds prevSpeed;

    private HgihlightSpeed speedText;

    // Start is called before the first frame update
    void Start()
    {
        playerFlyingComponent = this.GetComponent<Flying>();
        playerFlightControls = this.GetComponentInChildren<PlayerFlightControls>();

        prevSpeed = playerFlightControls.currentSpeedSetting;
        SetSpeed(prevSpeed);

        speedText = this.GetComponent<HgihlightSpeed>();
        speedText.HighlightSpeed(prevSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (prevSpeed != playerFlightControls.currentSpeedSetting)
        {
            SetSpeed(playerFlightControls.currentSpeedSetting);
            speedText.HighlightSpeed(playerFlightControls.currentSpeedSetting);
            prevSpeed = playerFlightControls.currentSpeedSetting;
        }
    }

    private void SetSpeed(ESpeeds index)
    {
        // rotate to position
        //Quaternion rotation = speedPointer.transform.localRotation;
        //speedPointer.transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotations[(int)index]);
        //Debug.Log($"Setting rotation from {speedPointer.transform.localRotation} to { speedPointer.transform.localRotation}");
        speedPointer.GetComponent<RotationZTween>().TweenRotation(rotations[(int)index]);
    }
}
