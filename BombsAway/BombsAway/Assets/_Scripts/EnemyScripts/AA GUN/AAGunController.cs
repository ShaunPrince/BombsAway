using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunController : MonoBehaviour
{
    public EAAGunState currentState;
    public AAGunShooting gunShooting;
    public Transform target;
    public AAGunAiming aim;

    public int numBulletsOnLowAlt;
    public int numBulletsOnMedAlt;

    public float radius;


    private void Awake()
    {
        this.transform.GetChild(2).localScale = new Vector3 (radius * 2,8000,radius * 2);
        this.transform.GetChild(2).rotation = Quaternion.identity;
        this.transform.GetChild(3).localScale = new Vector3(radius * 2, 8000, radius * 2);
        this.transform.GetChild(3).rotation = Quaternion.identity;
    }

    private void Update()
    {
        this.transform.GetChild(2).rotation = Quaternion.identity;
        this.transform.GetChild(3).rotation = Quaternion.identity;
        Tick();
    }

    public void Tick()
    {
        SetFiringState();
        switch (currentState)
        {
            case EAAGunState.Idle:
                break;

            case EAAGunState.ShootingLowAlt:
                gunShooting.bulletsPerBurst = numBulletsOnLowAlt;
                gunShooting.ShootBurst();
                break;

            case EAAGunState.ShootingMedAlt:
                gunShooting.bulletsPerBurst = numBulletsOnMedAlt;
                gunShooting.ShootBurst();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDamageEntity possibleTarget = other.GetComponentInParent<PlayerDamageEntity>();
        if(possibleTarget != null)
        {
            target = possibleTarget.transform;
            aim.targetRB = target.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerDamageEntity possibleTarget = other.GetComponentInParent<PlayerDamageEntity>();
        if (possibleTarget != null)
        {
            target = null;
            currentState = EAAGunState.Idle;
            aim.targetRB = null;
        }
    }

    public void SetFiringState()
    {
        if (target != null)
        {

            if (target.GetComponentInChildren<PlayerFlightControls>().currentAltSetting == EAlts.Low)
            {
                currentState = EAAGunState.ShootingLowAlt;
            }

            else if (target.GetComponentInChildren<PlayerFlightControls>().currentAltSetting == EAlts.Mid)
            {
                currentState = EAAGunState.ShootingMedAlt;
            }
            else
            {
                currentState = EAAGunState.Idle;
            }
        }
        else
        {
            currentState = EAAGunState.Idle;
        }
    }

}
