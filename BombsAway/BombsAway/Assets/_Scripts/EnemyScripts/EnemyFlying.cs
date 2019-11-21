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

    public float visionDistance;

    public float parallelDistance;
    //public float timeToRemainParallel;  // so not infinity parallel?
    private bool startParalellMovement = false;
    private bool currentlyParallelToPlayer = false;

    public float dodgeDistance;
    private float dodgeAmount = 100;
    public EDodgeType startDodging = EDodgeType.False;
    private GameObject currentlyDodgingObject;
    private bool currentlyDodging = false;

    [Range(0,1)]
    public float catchupPercentage;
    private bool needToCatchUp = true;

    //public float enemyNormalSpeed;

    private Transform playerTransform;
    private Flying playerFlyingComponent;
    private Flying enemyFlyingComponent;

    public float timeBetweenUpdates;
    private float deltaTime = 0f;
    private EStationID selectedStation;

    public Transform GetPlayerPosition()
    {
        return playerTransform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyFlyingComponent = this.GetComponent<Flying>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerFlyingComponent = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
        //this.GetComponentsInChildren<SphereCollider>().radius = dodgeDistance;    FIX DODGE TRIGGER RADIUS LATER
        SetAltitude();
        SetDirection();
        //SetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // constantly update location to which to fly to (location of player)
        // constantly update altitude untill it is within a certain range of the player?
        // when enemy gets too close to player, raise up, fly over, fly forward for a short time, then return to attacking
        if (deltaTime >= timeBetweenUpdates)
        {
            CheckPlayerDistance();
            SetAltitude();
            SetDirection();
            SetSpeed();
            CheckParallelism();
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

    public bool IsDodging()
    {
        return currentlyDodging;
    }

    public void SetDodging(EDodgeType dodgeType, GameObject dodgingObject)
    {
        startDodging = dodgeType;
        currentlyDodgingObject = dodgingObject;
    }

    public void SetDodging(EDodgeType dodgeType, GameObject dodgingObject, bool currentDodging)
    {
        startDodging = dodgeType;
        currentlyDodgingObject = dodgingObject;
        currentlyDodging = currentDodging;
    }

    public GameObject GetCurrentlyDodgingObject()
    {
        return currentlyDodgingObject;
    }

    private void CheckPlayerDistance()
    {
        // Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= dodgeDistance
        // if dodging
        if (currentlyDodging || startDodging != EDodgeType.False)
        {
            needToCatchUp = false;
            startParalellMovement = false;
            currentlyParallelToPlayer = false;
        }
        // if within parallel distance
        else if (!currentlyDodging && 
                 Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= parallelDistance)
        {
            startParalellMovement = true;
            needToCatchUp = false;
            currentlyDodging = false;

        }
        // if within vision distance
        //else if (!currentlyDodging &&
        //         Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= visionDistance)
        //{
        //    needToCatchUp = false;
        //}
        // if outside vision distance
        else
        {
            needToCatchUp = true;

            startDodging = EDodgeType.False;
            currentlyDodging = false;

            startParalellMovement = false;
            currentlyParallelToPlayer = false;
        }
    }

    #region Altitude
    private void SetAltitude()
    {
        // move towards player
        // if too close to player, go up/down to avoid them
        float altitude;
        // MAKE IT SO THEY CAN DODGE ANYONE
        // WHAT IF PLAYER IS MOVING UP WHILE ENEMY IS DODGIN UP OR VISVERSA
        if (startDodging != EDodgeType.False)
        {
            altitude = Dodge();
        }
        else if (startParalellMovement)
        {
            altitude = MatchPlayersAltitude();
        }
        else
        {
            
            altitude = FlyAroundPlayersAltitude();
        }
        
        enemyFlyingComponent.SetDesAlt(altitude);
    }

    private float Dodge()
    {
        float altitude;
        // if dodge altitude has not yet been set, set it
        // if dodging player, have option to go over or under
        if (!currentlyDodging && startDodging == EDodgeType.Player)
        {
            // if altitude is >= to player and close enough, fly up and over
            // if altitude is <= player and close enough, fly down and under
            if (enemyFlyingComponent.desireAltitude >= playerTransform.position.y) altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
            else altitude = enemyFlyingComponent.desireAltitude - dodgeAmount;

            //Debug.Log($"Dodging player! Avoidance Distance of {Mathf.Abs(playerTransform.position.x - this.transform.position.x)} or {Mathf.Abs(playerTransform.position.z - this.transform.position.z)}");
            currentlyDodging = true;
        }
        // if dodging other enemy
        // keep updating even if currently dodging
        else if (!currentlyDodging && startDodging == EDodgeType.OtherEnemy)
        {
            //if (Random.Range(0,1) < .5) altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
            //else altitude = enemyFlyingComponent.desireAltitude - dodgeAmount;
            if (currentlyDodgingObject)
            {
                //Debug.Log(currentlyDodgingObject);
                float enemyAlt = currentlyDodgingObject.GetComponentInParent<Flying>().desireAltitude;
                if (enemyFlyingComponent.currentAltitude < enemyAlt) altitude = enemyFlyingComponent.desireAltitude - dodgeAmount;
                else altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
                currentlyDodging = true;
            }
            // technically if this happens, should stop dodging
            else altitude = enemyFlyingComponent.desireAltitude;


        }
        // if dodging anything stationary, go over
        else if (!currentlyDodging && startDodging == EDodgeType.StationaryObject)
        {
            altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
            currentlyDodging = true;
        }
        else
        {
            // else continue dodging
            altitude = enemyFlyingComponent.desireAltitude;
        }

        return altitude;
    }

    private float MatchPlayersAltitude()
    {
        // get players current altitude
        return playerFlyingComponent.currentAltitude;
    }

    private float FlyAroundPlayersAltitude()
    {
        // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
        float maxAlt = playerTransform.position.y + percisionOffset;
        float minAlt = playerTransform.position.y - percisionOffset;

        return Random.Range(minAlt, maxAlt);
    }
    #endregion

    #region Direction
    private void SetDirection()
    {
        float direction;
        if (startParalellMovement)
        {
            direction = MatchPlayersDirection();
        }
        // if dodging, move in the same direction until no longer dodging
        else if (currentlyDodging)
        {
            direction = enemyFlyingComponent.currentDir;
        }
        else
        {
            direction = FlyTowardsPlayer();

            //Debug.Log($"Original rotation: {rotation}, {rotation.eulerAngles}   minOffset: {minDirection}, maxOffset: {maxDirection}\nChoosenDirection: {direction}");
        }

        enemyFlyingComponent.SetDesDir(direction);
    }

    private float MatchPlayersDirection()
    {
        // get players current direction
        // start turning to match that direction
        // update constantly to match player's direction for a certain amount of time
        return playerFlyingComponent.currentDir;
    }

    private float FlyTowardsPlayer()
    {
        // set desired direction to be towards the player (x axis)
        // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - this.transform.position, Vector3.up);
        float eulerDirection = rotation.eulerAngles.y;
        float maxDirection = (eulerDirection + percisionOffset);
        float minDirection = (eulerDirection - percisionOffset);

        return (Random.Range(minDirection, maxDirection));
    }
    #endregion

    #region Speed
    private void SetSpeed()
    {
        float speed;
        // allow enemy to be a bit faster when the first spawn in order to catch up to the player
        if (needToCatchUp)
        {
            speed = playerFlyingComponent.desiredForwardSpeed + (playerFlyingComponent.desiredForwardSpeed * catchupPercentage);
        }
        // slow down if dodging player and player is slowing down
        else if (currentlyDodging && startDodging == EDodgeType.Player)
        {
            speed = ChangeSpeedBasedOnPlayerWhileDodging();
        }
        // if dodging another player, speed up to do so
        else if (currentlyDodging && startDodging == EDodgeType.OtherEnemy)
        {
            speed = playerFlyingComponent.desiredForwardSpeed + (playerFlyingComponent.desiredForwardSpeed * catchupPercentage);
        }
        // if starting to be parallel, start to slow down to player speed
        //else if (startParalellMovement)
        //{
        //    // start to get closer to the players speed but not equal to
        //    if (playerFlyingComponent.currentForwardSpeed >= enemyFlyingComponent.currentForwardSpeed) speed = enemyFlyingComponent.currentForwardSpeed + playerFlyingComponent.currentForwardSpeed / 2;
        //    else speed = enemyFlyingComponent.currentForwardSpeed - playerFlyingComponent.currentForwardSpeed / 2;
        //}
        else if (currentlyParallelToPlayer)
        {
            speed = playerFlyingComponent.currentForwardSpeed;
        }
        else
        {
            speed = playerFlyingComponent.desiredForwardSpeed;
        }

        enemyFlyingComponent.SetDesSpeed(speed);
    }

    private float ChangeSpeedBasedOnPlayerWhileDodging()
    {
        if (playerFlyingComponent.desiredForwardSpeed < playerFlyingComponent.currentForwardSpeed)
        {
            return playerFlyingComponent.desiredForwardSpeed;
        }
        else return playerFlyingComponent.desiredForwardSpeed + (playerFlyingComponent.desiredForwardSpeed * catchupPercentage);
    }

    #endregion

    private void CheckParallelism()
    {
        int offset = 5;
        if (startParalellMovement && startDodging == EDodgeType.False &&
            playerFlyingComponent.currentDir - offset <= enemyFlyingComponent.currentDir && enemyFlyingComponent.currentDir <= playerFlyingComponent.currentDir + offset &&
            playerFlyingComponent.currentAltitude - offset <= enemyFlyingComponent.currentAltitude && enemyFlyingComponent.currentAltitude <= playerFlyingComponent.currentAltitude + offset)
        {
            currentlyParallelToPlayer = true;
            //Debug.Log($"Enemy is now parallel to player, ready to start shooting");
        }
        else currentlyParallelToPlayer = false;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
    try
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, visionDistance);

        // draw enemy parallel distance
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(this.transform.position, this.transform.up, parallelDistance);
    }
    catch
    {

    }

#endif
    }
}
