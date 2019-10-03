using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlightControls : ControlScheme
{
    public float[] presetSpeeds;
    public float[] presetAlts;

    public enum ESpeeds {Slow, Med, Fast, Full};
    public enum EAlts { Low, Mid, High, Soaring };

    public ESpeeds currentSpeedSetting;
    public EAlts currentAltSetting;


    public Flying fly;
    // Start is called before the first frame update
    void Start()
    {
        fly.setDesAlt(presetAlts[(int)currentAltSetting]);
        fly.setDesSpeed(presetSpeeds[(int)currentSpeedSetting]);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAltChange();
    }

    public void CheckForAltChange()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if((int)currentAltSetting < (int)EAlts.Soaring)
            {
                currentAltSetting += 1;
                fly.setDesAlt(presetAlts[(int)currentAltSetting]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if((int)currentAltSetting > (int) EAlts.Low)
            {
                currentAltSetting -= 1;
                fly.setDesAlt(presetAlts[(int)currentAltSetting]);

            }
        }
    }

}
