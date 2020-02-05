using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegableEnemy : DamageableEntity
{
    //private EnemyHit enemyHitScript;
    public float delayedDeathTime;
    public GameObject deathExplosion;

    private bool isDying = false;

    private void Start()
    {
        //enemyHitScript = this.GetComponent<EnemyHit>();
    }

    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance && health >0)
        {
            health -= incomingDamage;

            if (this.GetComponent<ColorTweening>()) this.GetComponent<ColorTweening>().FlashWhite();
            else if (this.GetComponent<ShaderTweening>()) this.GetComponent<ShaderTweening>().FlashWhite();
            else if (this.GetComponent<EnemyHit>()) this.GetComponent<EnemyHit>().VisuallyShowEnemyHit();

            //enemyHitScript.FlashWhite();
            //enemyHitScript.VisuallyShowEnemyHit();
            //Debug.Log(this + " Is taking damage");
            if (health <= 0 && !isDying)
            {
                Die();
                isDying = true;

            }
        }

    }

    public bool IsEnemyDying()
    {
        return isDying;
    }

    public void Die()
    {
        EnemyFragmentOnDeath temp = this.GetComponent<EnemyFragmentOnDeath>();
        if (temp != null)
        {
            if (deathExplosion != null)
            {
                GameObject explosion = Instantiate(deathExplosion, this.transform.position, this.transform.rotation);
                GameObject.Destroy(explosion, 5f);
            }
            temp.Fragment();
            StartCoroutine(DelayCoroutine());

        }
        else if (deathExplosion != null)
        {
            GameObject explosion =  Instantiate(deathExplosion, this.transform.position, this.transform.rotation);
            //explosion.transform.localScale.Set(50f, 50f, 50f);
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(explosion, 5f);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    IEnumerator DelayCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delayedDeathTime);
        GameObject.Destroy(this.gameObject);

    }
}
