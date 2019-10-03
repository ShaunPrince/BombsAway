using System.Collections;
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

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {

        currentAltitude = this.transform.position.y;
        //set vertical velocity to the current velocity plus/minus the velocity per second (Acceleration)
        rb.velocity = new Vector3(rb.velocity.x,Mathf.Sign(desireAltitude - currentAltitude) * verticalAcceleration * Time.deltaTime,rb.velocity.z);
        
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
        desiredDir = newDir;
    }

    private float CalculateTurn()
    {
        //set abs value of direction between 0 and 306
        currentDir = this.transform.rotation.eulerAngles.y % 360;

        if(Mathf.Abs(desiredDir - currentDir) < turningRate*Time.deltaTime)
        {
            return currentDir;
        }

        //convert -dir to their positive equivalent
        if(currentDir < 0)
        {
            currentDir += 360;
        }


        float deltaDir;
        //needs to turn right
        if(currentDir < desiredDir)
        {
            if(desiredDir - currentDir <= 180)
            {
                deltaDir = desiredDir - currentDir;
            }
            else
            {
                deltaDir = currentDir - desiredDir;
            }

        }
        //left
        else
        {
            if(currentDir - desiredDir <= 180)
            {
                deltaDir = desiredDir - currentDir;
            }
            else
            {
                deltaDir = currentDir - desiredDir;
            }
        }

        return  currentDir + (Mathf.Sign(deltaDir) * turningRate * Time.deltaTime);


    }


}
