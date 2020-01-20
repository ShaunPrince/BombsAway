using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegableEnemy : DamageableEntity
{
    private ColorTweening enemyHitScript;

    private void Start()
    {
        enemyHitScript = this.GetComponent<ColorTweening>();
    }

    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance)
        {
            health -= incomingDamage;
            enemyHitScript.FlashWhite();
            //enemyHitScript.VisuallyShowEnemyHit();
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                Die();

            }
        }

    }

    public void Die()
    {
        EnemyFragmentOnDeath temp = this.GetComponent<EnemyFragmentOnDeath>();
        if (temp != null)
        {
            temp.Fragment();
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
