﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAltitude : MonoBehaviour
{

    [SerializeField]
    private LayerMask rayDownLM;
    [SerializeField]
    private Rigidbody rb;

    public float straitDownAlt;

    [SerializeField]
    Flying fly;
    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponentInParent<Rigidbody>();
    }



    public float calcStraightDownAlt(float desiredRelAlt)
    {
        RaycastHit hit;
        if(Physics.Raycast(rb.transform.position, Vector3.down , out hit,1000000, rayDownLM))
        {
            straitDownAlt = hit.distance;
            //offset the point by the velocity
            Vector3 forwardPoint = hit.point + Vector3.Scale(rb.velocity, new Vector3(1,0,1))/25 ;
            RaycastHit hitForward;
            if (Physics.Raycast(rb.transform.position, (forwardPoint - rb.transform.position), out hitForward, 1000000, rayDownLM))
            {
                Debug.DrawRay(rb.transform.position, (forwardPoint - rb.transform.position), Color.red);
                return ( hitForward.point.y + desiredRelAlt);
            }
            else
            {
                return (hit.point.y + desiredRelAlt);
            }
        }
        else
        {
            return -1000f;
        }
    }

    
}
