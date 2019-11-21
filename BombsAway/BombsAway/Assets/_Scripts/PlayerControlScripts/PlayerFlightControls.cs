using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlightControls : ControlScheme
{
    public float[] presetSpeeds;
    public float[] presetAlts;

    public ESpeeds currentSpeedSetting;
    public EAlts currentAltSetting;

    private DynamicAltitude da;
    public Flying fly;

    public float GetDynamicAlt()
    {
        return da.straitDownAlt;
    }

    // Start is called before the first frame update
    void Start()
    {
        da = this.gameObject.GetComponentInParent<DynamicAltitude>();
        fly = GameObject.FindGameObjectWithTag("PilotStation").GetComponent<Flying>();
        fly.SetDesAlt(da.calcStraightDownAlt(presetAlts[(int)currentAltSetting]));
        fly.SetDesSpeed( presetSpeeds[(int)currentSpeedSetting]);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAltChange();
        CheckForSpeedChange();
        CheckForTurning();
    }

    public void CheckForAltChange()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if((int)currentAltSetting < (int)EAlts.High)
            {
                currentAltSetting += 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if((int)currentAltSetting > (int) EAlts.Low)
            {
                currentAltSetting -= 1;

            }
        }
        fly.SetDesAlt(da.calcStraightDownAlt(presetAlts[(int)currentAltSetting]));
    }

    public void CheckForSpeedChange()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if ((int)currentSpeedSetting < (int) ESpeeds.Fast)
            {
                currentSpeedSetting += 1;
                fly.SetDesSpeed(presetSpeeds[(int)currentSpeedSetting]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if ((int)currentSpeedSetting > (int)ESpeeds.Slow)
            {
                currentSpeedSetting -= 1;
                fly.SetDesSpeed(presetSpeeds[(int)currentSpeedSetting]);

            }
        }
    }

    public void CheckForTurning()
    {
        if(Input.GetKey(KeyCode.A))
        {
            fly.TurnLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            fly.TurnRight();
        }
        else
        {
            fly.NoTurn();
        }
    }

}
