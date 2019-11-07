using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrash : MonoBehaviour
{
    private bool hasCrashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        // if the object that has collided with enemy is not a bullet, it has crashed
        // make big boom animation 👌
        if (collision.gameObject.tag != "Bullet")
        {
            hasCrashed = true;
            float remainingHealth = this.GetComponent<DamageableEntity>().health;
            this.GetComponent<DamageableEntity>().TakeDamage(remainingHealth + 100, EAllegiance.Player);   // make sure they die
        }
    }
}
