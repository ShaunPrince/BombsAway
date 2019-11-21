using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegableEnemy : DamageableEntity
{
    private EnemyHit enemyHitScript;

    private void Start()
    {
        enemyHitScript = this.GetComponent<EnemyHit>();
    }

    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance)
        {
            health -= incomingDamage;
            enemyHitScript.VisuallyShowEnemyHit();
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

    }
}
