﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public float currentForwardSpeed;
    public float desiredForwardSpeed;
    public float forwardAcceleration;

    public float currentAltitude;
    public float desireAltitude;
    public float verticalAcceleration;

    public float currentDir;
    public float desiredDir;
    public float turningRate;

    public float lateralMovementDamperScale;

    public float fixedAcceleration;
    public float maxVerticalVelocity;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if(fixedAcceleration == 0 || maxVerticalVelocity == 0)
        {
            fixedAcceleration = 50;
            maxVerticalVelocity = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {

        currentAltitude = this.transform.position.y;
        //set vertical velocity to the current velocity plus/minus the velocity per second (Acceleration)
        //if(Mathf.Abs(desireAltitude - currentAltitude) * verticalAcceleration   >= verticalAcceleration )
        //{
        //    rb.velocity = new Vector3(rb.velocity.x,Mathf.Sign(desireAltitude - currentAltitude) * verticalAcceleration * Time.deltaTime,rb.velocity.z);
        //}
        //else
        //{
        //    rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        //}

        float deltaAlt = desireAltitude - currentAltitude;
        float timeToStop = Mathf.Abs(rb.velocity.y) / fixedAcceleration;
        float distToStop = rb.velocity.y * timeToStop + -.5f * Mathf.Sign(rb.velocity.y) * fixedAcceleration * timeToStop * timeToStop;

        if (Mathf.Abs(deltaAlt) <= Mathf.Abs(distToStop))
        {
            rb.AddForce(0f, -1 * Mathf.Sign(rb.velocity.y) * fixedAcceleration, 0f);
        }
        else if (Mathf.Abs(rb.velocity.y) + Mathf.Abs(fixedAcceleration/50) < maxVerticalVelocity)
        {
            rb.AddForce(0f, Mathf.Sign(deltaAlt) * fixedAcceleration, 0f);
        }


        
        //add forward/backward force to reach desired speed
        currentForwardSpeed = this.transform.InverseTransformVector(rb.velocity).z;
        rb.AddForce(this.transform.forward * Mathf.Sign(desiredForwardSpeed - currentForwardSpeed) * forwardAcceleration * Time.deltaTime, ForceMode.VelocityChange);

        //Dampen lateral movement, aka drifting during turns
        rb.AddRelativeForce(-this.transform.InverseTransformVector(rb.velocity).x/lateralMovementDamperScale, 0f, 0f, ForceMode.VelocityChange);

        //Debug.Log(this.transform.InverseTransformDirection(rb.velocity).x);
        
        //apply rotation toward desired rotation
        rb.MoveRotation(Quaternion.Euler(0f, CalculateTurn(), 0f));
    }


    //Should add checks / limits to valid values?
    public void SetDesSpeed(float newSpeed)
    {
        desiredForwardSpeed = newSpeed;
    }

    public void SetDesAlt(float newAlt)
    {
        desireAltitude = newAlt;
    }

    public void TurnLeft()
    {
        SetDesDir(this.transform.rotation.eulerAngles.y - turningRate);
    }

    public void TurnRight()
    {
        SetDesDir(this.transform.rotation.eulerAngles.y + turningRate);
    }

    public void NoTurn()
    {
        SetDesDir(this.transform.rotation.eulerAngles.y);
    }

    public void SetDesDir(float newDir)
    {
        desiredDir = ConvertToPos360Dir(newDir);
    }

    //take a float and convert set it t0 its 0-360 degree equivalent
    public float ConvertToPos360Dir(float dir)
    {
        dir = dir % 360;
        dir += 360;
        return dir % 360;
    }

    private float CalculateTurn()
    {
        //set abs value of direction between 0 and 360
        currentDir = ConvertToPos360Dir(this.transform.rotation.eulerAngles.y);

        //if the difference is less than the turn rate, we would overshoot, so don't bother turning
        if (Mathf.Abs(desiredDir - currentDir) < turningRate * Time.deltaTime || Mathf.Abs(currentDir - desiredDir) < turningRate * Time.deltaTime)
        {
            return currentDir;
        }
        else
        {
            return  currentDir + (CalcCWorCCW(currentDir,desiredDir) * turningRate * Time.deltaTime);
        }



    }

    //Determine if it is shorter to go clockwise or counter clockwise to reach desired direction
    private int CalcCWorCCW(float currentDirection, float desiredDirection)
    {
        float cwDirDif;
        float ccwDirDif;

        cwDirDif = desiredDir - currentDir;

        cwDirDif = ConvertToPos360Dir(cwDirDif);

        ccwDirDif = 360 - cwDirDif;

        if(cwDirDif < ccwDirDif)
        {
            //means we will apply a positive turn rate, thus turning clockwise
            return 1;
        }
        else if(cwDirDif > ccwDirDif)
        {
            //...CCW
            return -1;
        }
        else
        {
            return 0;
        }
    }


}
