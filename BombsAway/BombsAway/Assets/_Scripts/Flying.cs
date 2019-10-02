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
        //ser vertical velocity to the velocity per second (Acceleration)
        rb.velocity = new Vector3(rb.velocity.x,Mathf.Sign(desireAltitude - currentAltitude) * verticalAcceleration * Time.deltaTime,rb.velocity.z);
        
        currentForwardSpeed = this.transform.InverseTransformVector(rb.velocity).z;
        rb.AddForce(this.transform.forward * Mathf.Sign(desiredForwardSpeed - currentForwardSpeed) * forwardAcceleration * Time.deltaTime, ForceMode.VelocityChange);
    
        rb.MoveRotation(Quaternion.Euler())
    }

    private void calculateTurn()
    {
        //set abs value of direction between 0 and 306
        currentDir = this.transform.rotation.eulerAngles.y % 360;

        //convert -dir to their positive equivalent
        if(currentDir < 0)
        {
            currentDir += 360;
        }


        float deltaDir;
        //needs to turn right
        if(currentDir < desiredDir)
        {
            deltaDir = desiredDir - currentDir;
        }
        else
        {
            deltaDir = currentDir - desiredDir;
        }


    }


}
