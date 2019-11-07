using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buidling : DamageableEntity
{
    public EBuildingType buildingType;
    public int pointValue;

    // upon its death, update playerScore
    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance)
        {
            health -= incomingDamage;
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                MissionManager.IncreasePlayerScore(pointValue);
                GameObject.Destroy(this.gameObject);
            }
        }

    }
}


