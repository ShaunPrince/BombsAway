using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileContorller : MonoBehaviour
{
    private float missileDamage = 10f;
    [SerializeField]
    private Vector3 maxTorque;
    [SerializeField]
    private float increaseDistanceFromCenter;
    [SerializeField]
    private float maxDistanceFromCenter;
    private Transform missileModel;

    private float distanceToMaxRadius;

    private float timeAlive = 0f;
    private float maxTimeAlive = 50f;
    private Rigidbody rigidbody;

    private Vector3 startPos;
    private Vector3 playerPos;
    //private Vector3 lastPosition;
    //private Vector3 currentPosition;

    [SerializeField]
    private LayerMask layersToHit;

    public EAllegiance allegiance;

    // Start is called before the first frame update
    void Start()
    {
        //currentPosition = this.transform.position;
        //lastPosition = currentPosition;
        rigidbody = this.GetComponent<Rigidbody>();

        rigidbody.AddRelativeTorque(maxTorque, ForceMode.VelocityChange);

        startPos = this.transform.position;
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        missileModel = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeAlive >= maxTimeAlive)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //CheckForHit();

            Corkscrew();

            timeAlive += Time.deltaTime;
        }
    }

    void Corkscrew()
    {
        // if missile is less then half way to the player, start closing in
        if (Vector3.Distance(missileModel.position, playerPos) < Vector3.Distance(startPos, playerPos)/2 &&
            Vector3.Distance(missileModel.position, playerPos) <= distanceToMaxRadius)
        {
            // do not go below zero
            if (missileModel.localPosition.x - increaseDistanceFromCenter >= 0)
            {
                this.transform.GetChild(0).localPosition = new Vector3(missileModel.localPosition.x - increaseDistanceFromCenter, 0, 0);
                //Debug.Log($"Decreasing radius { this.transform.GetChild(0).localPosition}");
            }
        }
        // if missile is more then half way from player, spiral out
        else
        {
            if (missileModel.localPosition.x < maxDistanceFromCenter)
            {
                this.transform.GetChild(0).localPosition = new Vector3(missileModel.localPosition.x + increaseDistanceFromCenter, 0, 0);
                distanceToMaxRadius = Vector3.Distance(missileModel.position, startPos);
                //Debug.Log($"Increasing radius { this.transform.GetChild(0).localPosition}");
            }
        }

        //Debug.Log($"distance to max radius: {distanceToMaxRadius}, {Vector3.Distance(startPos, playerPos) / 2}");
    }
}
