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


            if (health <= 50)
            {
                FindObjectOfType<AudioManager>().PlayAlarm();  //need to fix creating instances
 
            }
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                //Load Game Over
                GameObject.Destroy(this.gameObject);
            }
            
        }
    }
}
