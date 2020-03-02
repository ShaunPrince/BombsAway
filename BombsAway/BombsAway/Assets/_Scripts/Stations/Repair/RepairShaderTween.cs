using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShaderTween : MonoBehaviour
{
    public GameObject partToTween;

    private float fadeTime = .4f;
    private float maxSteam;
    private float prevDensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDensityAndSteam(float max, float initial)
    {
        maxSteam = max;
        prevDensity = initial;
        LerpSteam(prevDensity);
        if (prevDensity > 0)
        {

        }
        else
        {
            TurnOffDensity();
        }
    }

    public void AddSteam()
    {
        partToTween.GetComponent<Renderer>().material.SetFloat("Vector1_4FEBF9CE", 1f);
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", prevDensity, "to", maxSteam,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "LerpSteam"));
    }

    public void ReleaseSteam()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", prevDensity, "to", 0f,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "LerpSteam"));
    }

    public void TurnOffDensity()
    {
        LerpDensity(0f);
    }

    public bool SteamTurnedOff()
    {
        if (Mathf.Approximately(prevDensity, 0f)) return true;
        else return false;
    }

    private void LerpSteam(float amount)
    {
        partToTween.GetComponent<Renderer>().material.SetFloat("Vector1_BA90D095", amount);
        prevDensity = amount;
    }

    private void LerpDensity(float amount)
    {
        partToTween.GetComponent<Renderer>().material.SetFloat("Vector1_4FEBF9CE", amount);
    }
}
