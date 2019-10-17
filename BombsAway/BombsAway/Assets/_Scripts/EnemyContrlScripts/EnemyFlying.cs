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

public class EnemyFlying : MonoBehaviour
{
    // auto set lateral movement damper scale
    public float percisionOffset; // in terms of flying towards player, altitude relative to player
                                  // don't want perfect aim

    public float parallelDistance;
    public float timeToRemainParallel;
    private bool startParalellMovement = false;
    private bool currentlyParallelToPlayer = false;

    public float dodgeDistance;
    private float dodgeAmount = 40;
    private bool startDodging = false;
    private bool currentlyDodging = false;

    private Transform playerTransform;
    private Flying playerFlyingComponent;
    private Flying flyingComponent;

    public float timeBetweenUpdates;
    private float deltaTime = 0f;

    public Transform GetPlayerPosition()
    {
        return playerTransform;
    }

    // Start is called before the first frame update
    void Start()
    {
        flyingComponent = this.GetComponent<Flying>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerFlyingComponent = GameObject.FindWithTag("Player").GetComponent<Flying>();
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

    public bool IsParallel()
    {
        return currentlyParallelToPlayer;
    }

    void CheckPlayerDistance()
    {
        if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) <= dodgeDistance &&
            Mathf.Abs(playerTransform.position.z - this.transform.position.z) <= dodgeDistance)
        {
            startDodging = true;
        }
        else if (Mathf.Abs(playerTransform.position.x - this.transform.position.x) <= parallelDistance &&
                 Mathf.Abs(playerTransform.position.z - this.transform.position.z) <= parallelDistance)
        {
            startParalellMovement = true;
        }
        else
        {
            startDodging = false;
            currentlyDodging = false;

            startParalellMovement = false;
            currentlyParallelToPlayer = false;
        }
    }

    void SetAltitude()
    {
        // move towards player
        // if too close to player, go up/down to avoid them
        float altitude;
        // MAKE IT SO THEY CAN DODGE ANYONE
        // WHAT IF PLAYER IS MOVING UP WHILE ENEMY IS DODGIN UP OR VISVERSA
        CheckPlayerDistance();
        if (startDodging)
        {
            altitude = DodgePlayer();
        }
        else if (startParalellMovement)
        {
            altitude = MatchPlayersAltitude(); //PullUpParallelToPlayer();
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

    float DodgePlayer()
    {
        float altitude;
        // if dodge altitude has not yet been set, set it, else do nothing
        if (!currentlyDodging)
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
            // else doooo nothing
            altitude = flyingComponent.desireAltitude;
        }

        return altitude;
    }

    float MatchPlayersAltitude()
    {
        return 0f;
    }

    float PullUpParallelToPlayer()
    {
        // get players current direction
        // start turning to match that direction
        // update constantly to match player's direction for a certain amount of time
        return playerFlyingComponent.currentDir;
    }

    void SetDirection()
    {
        float direction;
        if (currentlyParallelToPlayer)
        {
            direction = MatchPlayersDirection();
        }
        // if dodging?
        else
        {
            // set desired direction to be towards the player (x axis)
            // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
            Quaternion rotation = Quaternion.LookRotation(playerTransform.position - this.transform.position, Vector3.up);
            float eulerDirection = rotation.eulerAngles.y;
            float maxDirection = (eulerDirection + percisionOffset);
            float minDirection = (eulerDirection - percisionOffset);

            direction = (Random.Range(minDirection, maxDirection));

            //Debug.Log($"Original rotation: {rotation}, {rotation.eulerAngles}   minOffset: {minDirection}, maxOffset: {maxDirection}\nChoosenDirection: {direction}");
        }

        flyingComponent.SetDesDir(direction);
    }

    private float MatchPlayersDirection()
    {
        return 0f;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draw enemy parallel distance
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, parallelDistance);

        // draw enemy dodging distance
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, dodgeDistance);
#endif
    }
}
