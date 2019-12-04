using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageEntity : DamageableEntity
{
    public RepairSystemManager repSysMan;
    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance)
        {
            if (allegianceOfIncomingDamage == EAllegiance.Enemy)
            {
                repSysMan.RollForSteamLoss();
            }
            health -= incomingDamage;
            FindObjectOfType<AudioManager>().PlayGotHit();
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                //Load Game Over
                FindObjectOfType<AudioManager>().Play("GameOver");
                FindObjectOfType<AudioManager>().Play("GunBattle");

                GameObject.Destroy(this.gameObject);
            }
            if(health <= 10)
            {
                FindObjectOfType<AudioManager>().Play("EmergencyAlarm");

            }

        }
    }
}
