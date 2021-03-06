﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObject : DamageableEntity
{
    public ETerrainObjectType objectType;
    public int pointValue;

    // upon its death, update playerScore
    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance )
        {
            health -= incomingDamage;
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                MissionManager.IncreasePlayerScore(pointValue);

                if (objectType == ETerrainObjectType.Target) MissionManager.DecreaseTargetCount();

                GameObject.Destroy(this.gameObject);
            }
        }

    }
}


