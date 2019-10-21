using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevation : MonoBehaviour
{
    public Rigidbody rb;
    public Transform destination;

    //public float accerationDistance;

    //public float verticalAccerationScaler;

    //public float desiredVerticalVelocity;

    //public float maxUpwardVelocity;

    //public float maxDownwardVelocity;

    public float timeBetweenAccStartAndArrive;

    public float acceleration = 2;

    public float distanceToStartAcc;

    int iteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = this.transform.up  ;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.DrawRay(this.transform.position, (destination.position - this.transform.position) / verticalForce);
    }

    private void FixedUpdate()
    {

        
        Debug.Log(iteration.ToString() + " : " + rb.velocity.y);
        ++iteration;
        float curSign = Mathf.Sign(acceleration);
        float newSign = Mathf.Sign(Mathf.Sign(this.transform.position.y - destination.position.y));


        if(Mathf.Abs(destination.position.y - this.transform.position.y) < distanceToStartAcc) //&& curSign != newSign)
        {
 
            acceleration =  -(2*distanceToStartAcc / (timeBetweenAccStartAndArrive*timeBetweenAccStartAndArrive));
        }

        rb.AddForce(0, acceleration, 0, ForceMode.Acceleration);

        //if(true)
        //{
        //    if (rb.velocity.y >= maxUpwardVelocity || rb.velocity.y <= -maxDownwardVelocity)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        rb.AddForce(0, verticalAccerationScaler, 0, ForceMode.Acceleration);
        //    }

        //}

    }
}
