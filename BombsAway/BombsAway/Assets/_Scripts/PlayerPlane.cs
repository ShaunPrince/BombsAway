using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlane : DamageableEntity
{
    public enum EAltitude { Low , Med , High , VeryHigh }
    public enum ESpeed { Slow, Med, Fast, VeryFast}

    public float[] presetAlts;
    public float[] presetSpeeds;
    public float currentAltLevel;
    public float destAltLevel;
    public float currentSpeedLevel;
    public float destSpeedLevel;

    public Rigidbody rb;

    public float ForwardOffsetAimPoint;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentAltLevel = this.transform.position.y;
    }

    public void CheckForAndApplyAltChange()
    {

    }

    public void SetDestAlt(EAltitude newAlt)
    {
        destAltLevel = presetAlts[(int)newAlt];
    }

    public void SetDestSpeed(ESpeed newSpeed)
    {
        destSpeedLevel = presetAlts[(int)newSpeed];
    }

    void FixedUpdate()
    {
        rb.MovePosition(this.transform.position + (this.transform.forward * ForwardOffsetAimPoint + this.transform.up * (destAltLevel-currentAltLevel)) * Time.deltaTime);
    }


}
