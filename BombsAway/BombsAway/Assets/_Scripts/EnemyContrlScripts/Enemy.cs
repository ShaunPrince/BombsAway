using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
scrip takes care of the randomness - so we dont have to pass variables
simple enemy command script

random spawn
altitude height of player
direction to player from enemy - const update
desired speed (random)

gets to close? go over and fly away and turn around a little bit later to re-attack

damageable entity - script
take damage - health stuffs

just fly towards player for now

use shaun's script - flying script

*/

public class Enemy : DamageableEntity
{
    // auto set lateral movement damper scale
    public float percisionOffset; // in terms of flying towards player, altitude relative to player
                                  // don't want perfect aim
                       
    public float avoidanceDistance;
    private float dodgeAmount = 1;
    private bool currentlyDodging = false;

    private Transform playerTransform;
    private Flying flyingComponent;

    public float timeBetweenUpdates;
    private float deltaTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        flyingComponent = this.GetComponent<Flying>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        SetAltitude();
        SetDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // constantly update location to which to fly to (location of player)
        // constantly update altitude untill it is within a certain range of the player?
        // when enemy gets too close to player, raise up, fly over, fly forward for a short time, then return to attacking
        if (deltaTime >= timeBetweenUpdates)
        {
            SetAltitude();
            SetDirection();
            deltaTime = 0;
        }
        else
        {
            deltaTime += Time.deltaTime;
        }
    }

    void CheckAvoidPlayer()
    {
        if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) <= avoidanceDistance &&
            Mathf.Abs(playerTransform.position.z - this.transform.position.z) <= avoidanceDistance)
        {
            currentlyDodging = true;
        }
        else
        {
            currentlyDodging = false;
        }
    }

    void SetAltitude()
    {
        // move towards player
        // if too close to player, go up to avoid them
        float altitude;
        CheckAvoidPlayer();
        if (currentlyDodging)
        {
            // if altitude is >= to player and close enough, fly up and over
            // if altitude is <= player and close enough, fly down and under
            if (flyingComponent.desireAltitude >= playerTransform.position.y) altitude = flyingComponent.desireAltitude + dodgeAmount;
            else altitude = flyingComponent.desireAltitude - dodgeAmount;

            Debug.Log($"Dodging player! Avoidance Distance of {Mathf.Abs(playerTransform.position.x - this.transform.position.x)} or {Mathf.Abs(playerTransform.position.z - this.transform.position.z)}");
            currentlyDodging = true;
        }
        else
        {
            // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
            float maxAlt = playerTransform.position.y + percisionOffset;
            float minAlt = playerTransform.position.y - percisionOffset;
            altitude = Random.Range(minAlt, maxAlt);
        }
        
        flyingComponent.SetDesAlt(altitude);
    }

    void SetDirection()
    {
        // set desired direction to be towards the player (x axis)
        // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - this.transform.position, Vector3.up);
        float eulerDirection = rotation.eulerAngles.y;
        float maxDirection = (eulerDirection + percisionOffset);
        float minDirection = (eulerDirection - percisionOffset);
        
        float direction = (Random.Range(minDirection, maxDirection)) % 360;

        //Debug.Log($"Original rotation: {rotation}, {rotation.eulerAngles}   minOffset: {minDirection}, maxOffset: {maxDirection}\nChoosenDirection: {direction}");

        flyingComponent.SetDesDir(direction);
    }
}
