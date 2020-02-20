using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunController : MonoBehaviour
{
    public EAAGunState currentState;
    public AAGunShooting gunShooting;
    public Transform target;
    public AAGunAiming aim;

    private void Update()
    {
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
                gunShooting.ShootBurst();
                break;

            case EAAGunState.ShootingMedAlt:
                gunShooting.ShootBurst();
                //Shoot Missles Only
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
