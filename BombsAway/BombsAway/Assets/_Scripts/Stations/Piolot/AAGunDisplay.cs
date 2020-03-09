using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AAGunDisplay : MonoBehaviour
{
    public GameObject[] bulletSprites;
    public GameObject dangerSign;
    public PlayerFlightControls playerControlls;
    public GameObject allAAguns;

    private EAlts prevAlt;
    private float time = 2f;

    private bool prevGunStatus = false;

    private bool intialSet = true;
    private bool WHITE = true;

    // Start is called before the first frame update
    void Start()
    {
        prevAlt = playerControlls.currentAltSetting;
        //SetAAGunDisplay(prevAlt);
    }

    // Update is called once per frame
    void Update()
    {
        if (intialSet)
        {
            SetAAGunDisplay(prevAlt);
            intialSet = false;
        }
        if (prevAlt != playerControlls.currentAltSetting)
        {
            SetAAGunDisplay(playerControlls.currentAltSetting);
            prevAlt = playerControlls.currentAltSetting;
        }
        if (prevGunStatus != AAgunWithinRange())
        {
            //Debug.Log("AA guns");
            dangerSign.GetComponent<SignBlink>().Blink(AAgunWithinRange());
            prevGunStatus = AAgunWithinRange();
        }
    }

    private void SetAAGunDisplay(EAlts alt)
    {
        if (alt == EAlts.Low)
        {
            foreach (GameObject bullet in bulletSprites)
            {
                bullet.GetComponent<BlackWhiteTween>().TweenColor(WHITE, time);
            }
        }
        else if (alt == EAlts.Mid)
        {
            for (int i = 0; i < bulletSprites.Length; i++)
            {
                if (i < 1)
                    bulletSprites[i].GetComponent<BlackWhiteTween>().TweenColor(WHITE, time);
                else
                    bulletSprites[i].GetComponent<BlackWhiteTween>().TweenColor(!WHITE, time);
            }
        }
        else
        {
            foreach (GameObject bullet in bulletSprites)
            {
                bullet.GetComponent<BlackWhiteTween>().TweenColor(!WHITE, time);
            }
        }
    }

    private bool AAgunWithinRange()
    {
        for (int i = 0; i < allAAguns.transform.childCount; i++)
        {
            if (allAAguns.transform.GetChild(i).GetComponent<AAGunController>().currentState != EAAGunState.Idle)
            {
                return true;
            }
        }

        return false;
    }

}
