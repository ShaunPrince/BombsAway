using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrash : MonoBehaviour
{
    public bool hasCrashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        // if the object that has collided with enemy is not a bullet, it has crashed
        // make big boom animation 👌
        if (collision.gameObject.tag != "Bullet")
        {
            hasCrashed = true;
            float remainingHealth = this.GetComponent<EnemyShooting>().health;
            this.GetComponent<EnemyShooting>().TakeDamage(remainingHealth + 100, EAllegiance.Player);   // make sure they die
        }
    }
}
