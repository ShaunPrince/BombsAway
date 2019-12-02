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
    public float dodgeDistance;

    private float dodgeAmount = 100;
    public EDodgeType dodgeType = EDodgeType.Nothing;
    private GameObject currentlyDodgingObject;

    [Range(0, 1)]
    public float catchupPercentage;

    private Transform playerTransform;
    private Flying playerFlyingComponent;
    private Flying enemyFlyingComponent;

    // FLYING UPDATES TIMER
    public float timeBetweenUpdates;
    private float deltaTime = 0f;

    // PARALLEL TIMER
    [SerializeField]
    private Vector2 swoopInTimeRange;
    private float swoopInTime = -1f;
    private float swoopDeltaTime = 0f;

    // RUNNING AWAY TIMER
    [SerializeField]
    private Vector2 runawayTimeRange;
    private float runawayTime = 5f;
    private float runawayDeltaTime = 0f;

    [SerializeField]
    private EEnemyAction currentEnemyAction;
    private EEnemyAction prevEnemyAction;

    public Transform GetPlayerPosition()
    {
        return playerTransform;
    }

    public bool IsParallel()
    {
        if (currentEnemyAction == EEnemyAction.currentlyParallelToPlayer) return true;
        else return false;
    }

    public bool IsWithinVisionRange()
    {
        return Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= visionDistance;
    }

    public bool IsDodging()
    {
        if (currentEnemyAction == EEnemyAction.currentlyDodging) return true;
        else return false;
    }

    public void SetDodging(EDodgeType dodgeType, GameObject dodgingObject, bool startDodging)
    {
        this.dodgeType = dodgeType;
        currentlyDodgingObject = dodgingObject;
        if (startDodging) currentEnemyAction = EEnemyAction.startDodging;
        else currentEnemyAction = EEnemyAction.neutralFlying;
    }

    public GameObject GetCurrentlyDodgingObject()
    {
        return currentlyDodgingObject;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        enemyFlyingComponent = this.GetComponent<Flying>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerFlyingComponent = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
        currentEnemyAction = EEnemyAction.neutralFlying;
        SetEnemysCurrentAction();
        CheckEnemysCurrentAction();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // constantly update location to which to fly to (location of player)
        // constantly update altitude untill it is within a certain range of the player?
        // when enemy gets too close to player, raise up, fly over, fly forward for a short time, then return to attacking
        if (deltaTime >= timeBetweenUpdates)
        {
            SetEnemysCurrentAction();
            CheckEnemysCurrentAction();
            deltaTime = 0;
        }
        else
        {
            deltaTime += Time.deltaTime;
        }
    }

    private void SetEnemysCurrentAction()
    {
        // if dodging
        if (currentEnemyAction == EEnemyAction.startDodging ||
            currentEnemyAction == EEnemyAction.currentlyDodging ||
            currentEnemyAction == EEnemyAction.startParallelMovement ||
            currentEnemyAction == EEnemyAction.currentlyParallelToPlayer ||
            currentEnemyAction == EEnemyAction.runningAwayFromPlayer)
        {
            // do nothing, just don't do any other action either
        }
        // if within parallel distance
        else if (Mathf.Abs(Vector3.Distance(playerTransform.position, this.transform.position)) <= parallelDistance)
        {
            currentEnemyAction = EEnemyAction.startParallelMovement;

            // set time to stay parallel
            if (swoopInTime == -1)
            {
                swoopInTime = Random.Range(swoopInTimeRange.x, swoopInTimeRange.y);
            }

        }
        else
        {
            currentEnemyAction = EEnemyAction.neutralFlying;
        }
    }

    /* neutralFlying,
       startDodging,
       currentlyDodging,
       startParalellMovement,
       currentlyParallelToPlayer
    */
    private void CheckEnemysCurrentAction()
    {
        //Debug.Log($"{this.transform.name}'s Current Enemy Action: {currentEnemyAction}");

        if (currentEnemyAction == EEnemyAction.neutralFlying)
        {
            SetNeutralFlying();
            ResetTimers();   // just in case
        }
        else if (currentEnemyAction == EEnemyAction.startDodging)
        {
            SetStartDodgingFlying();
            ResetTimers();
        }
        else if (currentEnemyAction == EEnemyAction.currentlyDodging)
        {
            SetCurrentlyDodging();
        }
        else if (currentEnemyAction == EEnemyAction.startParallelMovement)
        {
            SetStartParallelMovement();
            CheckParallelism();
        }
        else if (currentEnemyAction == EEnemyAction.currentlyParallelToPlayer)
        {
            SetCurrentlyParallel();
            ParallelismCountDown();
        }
        else if (currentEnemyAction == EEnemyAction.runningAwayFromPlayer)
        {
            SetRunningAwayFromPlayer();
            RunningAwayCountDown();
        }
        else
        {
            // something went wrong
            Debug.Log($"{this.transform.name} is trying to {currentEnemyAction} but that is not allowed, something has gone terribly wrong");
        }
    }

    #region Neutral Flying
    private void SetNeutralFlying()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(FlyAroundPlayersAltitude());
        // set direction
        enemyFlyingComponent.SetDesDir(FlyTowardsPlayer());
        // set speed
        enemyFlyingComponent.SetDesSpeed(EnemyNormalSpeed());
    }

    private float FlyAroundPlayersAltitude()
    {
        // chose a range between playerPos - percisionOffset and playerPos + percisionOffset
        float maxAlt = playerTransform.position.y + percisionOffset;
        float minAlt = playerTransform.position.y - percisionOffset;

        return Random.Range(minAlt, maxAlt);
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

    private float EnemyNormalSpeed()
    {
        return playerFlyingComponent.desiredForwardSpeed + (playerFlyingComponent.desiredForwardSpeed * catchupPercentage);
    }
    #endregion

    #region Dodging
    private void SetStartDodgingFlying()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(StartDodgingAlt());
        // set direction
        enemyFlyingComponent.SetDesDir(FlyTowardsPlayer());
        // set speed
        enemyFlyingComponent.SetDesSpeed(DodgingSpeed());

        currentEnemyAction = EEnemyAction.currentlyDodging;
    }

    private void SetCurrentlyDodging()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(enemyFlyingComponent.desireAltitude);
        // set direction
        enemyFlyingComponent.SetDesDir(enemyFlyingComponent.currentDir);
        // set speed
        enemyFlyingComponent.SetDesSpeed(DodgingSpeed());
    }

    private float StartDodgingAlt()
    {
        float altitude;
        // if dodging player, have option to go over or under
        if (dodgeType == EDodgeType.Player)
        {
            // if altitude is >= to player and close enough, fly up and over
            // if altitude is <= player and close enough, fly down and under
            if (enemyFlyingComponent.desireAltitude >= playerTransform.position.y) altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
            else altitude = enemyFlyingComponent.desireAltitude - dodgeAmount;

            //Debug.Log($"Dodging player! Avoidance Distance of {Mathf.Abs(playerTransform.position.x - this.transform.position.x)} or {Mathf.Abs(playerTransform.position.z - this.transform.position.z)}");
        }
        // if dodging other enemy
        // keep updating even if currently dodging
        else if (dodgeType == EDodgeType.OtherEnemy)
        {
            if (currentlyDodgingObject)
            {
                float enemyAlt = currentlyDodgingObject.GetComponentInParent<Flying>().desireAltitude;
                if (enemyFlyingComponent.currentAltitude < enemyAlt) altitude = enemyFlyingComponent.desireAltitude - dodgeAmount;
                else altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
            }
            // technically if this happens, should stop dodging
            else altitude = enemyFlyingComponent.desireAltitude;


        }
        // if dodging anything stationary, go over
        else if (dodgeType == EDodgeType.StationaryObject)
        {
            altitude = enemyFlyingComponent.desireAltitude + dodgeAmount;
        }
        else
        {
            // something went wrong
            altitude = enemyFlyingComponent.desireAltitude;
            Debug.Log($"{this.transform.name} is trying to dodge {dodgeType} but that is not allowed, something has gone terribly wrong");
        }

        return altitude;
    }

    private float DodgingSpeed()
    {
        float speed;
        // slow down if dodging player and player is slowing down
        if (dodgeType == EDodgeType.Player)
        {
            speed = ChangeSpeedBasedOnPlayerWhileDodging();
        }
        // if dodging another player, speed up to do so
        else if (dodgeType == EDodgeType.OtherEnemy)
        {
            speed = playerFlyingComponent.desiredForwardSpeed + (playerFlyingComponent.desiredForwardSpeed * catchupPercentage);
        }
        // dodging anything else, go normal speed
        else
        {
            speed = playerFlyingComponent.desiredForwardSpeed;
        }

        return speed;
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

    #region Parallel 
    private void SetStartParallelMovement()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(MatchPlayersAltitude());
        // set direction
        enemyFlyingComponent.SetDesDir(MatchPlayersDirection());
        // set speed
        enemyFlyingComponent.SetDesSpeed(playerFlyingComponent.desiredForwardSpeed);
    }

    private void SetCurrentlyParallel()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(FlyAroundPlayersAltitude());
        // set direction
        enemyFlyingComponent.SetDesDir(MatchPlayersDirection());
        // set speed
        enemyFlyingComponent.SetDesSpeed(playerFlyingComponent.currentForwardSpeed);
    }

    private float MatchPlayersAltitude()
    {
        // get players current altitude
        return playerFlyingComponent.currentAltitude;
    }

    private float MatchPlayersDirection()
    {
        // get players current direction
        // start turning to match that direction
        // update constantly to match player's direction for a certain amount of time
        return playerFlyingComponent.currentDir;
    }

    private void CheckParallelism()
    {
        int offset = 5;
        if (currentEnemyAction == EEnemyAction.startParallelMovement &&
            playerFlyingComponent.currentDir - offset <= enemyFlyingComponent.currentDir && enemyFlyingComponent.currentDir <= playerFlyingComponent.currentDir + offset &&
            playerFlyingComponent.currentAltitude - offset <= enemyFlyingComponent.currentAltitude && enemyFlyingComponent.currentAltitude <= playerFlyingComponent.currentAltitude + offset)
        {
            currentEnemyAction = EEnemyAction.currentlyParallelToPlayer;
        }
    }

    private void ParallelismCountDown()
    {
        if (currentEnemyAction == EEnemyAction.currentlyParallelToPlayer)
        {
            if (swoopDeltaTime >= swoopInTime)
            {
                currentEnemyAction = EEnemyAction.runningAwayFromPlayer;
                runawayTime = Random.Range(runawayTimeRange.x, runawayTimeRange.y);

                swoopInTime = -1;
                swoopDeltaTime = 0f;
            }
            else
            {

                swoopDeltaTime += Time.deltaTime;
                //Debug.Log($"{this.transform.name} is now parallel to player, {swoopDeltaTime} : {swoopInTime}");
            }
        }
    }
    #endregion

    #region Running Away
    private void SetRunningAwayFromPlayer()
    {
        // set altitude
        enemyFlyingComponent.SetDesAlt(RandomRunawayAltitude());
        // set direction
        enemyFlyingComponent.SetDesDir(TurnAwayFromPlayer());
        // set speed
        enemyFlyingComponent.SetDesSpeed(EnemyNormalSpeed());
    }

    private float RandomRunawayAltitude()
    {
        float offset = 200f;
        if (Mathf.Approximately(runawayDeltaTime, 0) )
        {
            return Random.Range(playerFlyingComponent.desireAltitude - offset, playerFlyingComponent.desireAltitude + offset);
        }
        else return enemyFlyingComponent.desireAltitude;
    }

    private float TurnAwayFromPlayer()
    {
        float offset = 130f;
        if (Mathf.Approximately(runawayDeltaTime, 0))
        {
            EPosition enemySide = this.GetComponent<EnemyShootGun>().CurrentGunBeingShot();
            // if the enemy is to the left of the player
            if (enemySide == EPosition.Right)
            {
                return Random.Range(playerFlyingComponent.desiredDir - offset % 360, playerFlyingComponent.desiredDir);
            }
            // if the enemy is to the right of the player
            else if (enemySide == EPosition.Left)
            {
                return Random.Range(playerFlyingComponent.desiredDir, playerFlyingComponent.desiredDir + offset % 360);
            }
            else
            {
                return enemyFlyingComponent.desiredDir;
            }
        }
        else return enemyFlyingComponent.desiredDir;
    }

    private void RunningAwayCountDown()
    {
        if (runawayDeltaTime >= runawayTime)
        {
            currentEnemyAction = EEnemyAction.neutralFlying;
            runawayDeltaTime = 0f;
        }
        else
        {
            runawayDeltaTime += Time.deltaTime;
        }
    }
    #endregion

    private void ResetTimers()
    {
        swoopInTime = -1;
        swoopDeltaTime = 0f;

        runawayDeltaTime = 0f;
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
