using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlightControls : ControlScheme
{
    public float[] presetSpeeds;
    public float[] presetAlts;

    public ESpeeds currentSpeedSetting;
    public EAlts currentAltSetting;

    public PlayerAutopilot autoPilot;

    private DynamicAltitude da;
    public Flying fly;

    public bool turningLeft;

    public float GetDynamicAlt()
    {
        return da.straitDownAlt;
    }

    // Start is called before the first frame update
    void Awake()
    {
        da = this.gameObject.GetComponentInParent<DynamicAltitude>();
        fly = GameObject.FindGameObjectWithTag("PilotStation").GetComponent<Flying>();
        autoPilot = GameObject.FindObjectOfType<PlayerAutopilot>();
    }
    private void Start()
    {

        autoPilot.desiredAlt = (presetAlts[(int)currentAltSetting]);
        autoPilot.desiredSpeed = (presetSpeeds[(int)currentSpeedSetting]);
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
        if(Input.GetMouseButtonDown(0))
        {
            if((int)currentAltSetting < (int)EAlts.High)
            {
                currentAltSetting += 1;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if((int)currentAltSetting > (int) EAlts.Low)
            {
                currentAltSetting -= 1;

            }
        }
        autoPilot.desiredAlt = (presetAlts[(int)currentAltSetting]);

    }

    public void CheckForSpeedChange()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if ((int)currentSpeedSetting < (int) ESpeeds.Fast)
            {
                currentSpeedSetting += 1;

            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if ((int)currentSpeedSetting > (int)ESpeeds.Slow)
            {
                currentSpeedSetting -= 1;
                //fly.SetDesSpeed(presetSpeeds[(int)currentSpeedSetting]);

            }
        }
        autoPilot.desiredSpeed = (presetSpeeds[(int)currentSpeedSetting]);
    }

    public void CheckForTurning()
    {
        if(Input.GetKey(KeyCode.A))
        {
            fly.TurnLeft();
            turningLeft = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            fly.TurnRight();
            turningLeft = false;
        }
        else
        {
            fly.NoTurn();
        }
    }

}
