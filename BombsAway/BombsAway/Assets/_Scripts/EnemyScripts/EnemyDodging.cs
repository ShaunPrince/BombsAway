using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodging : MonoBehaviour
{
    private EnemyFlying enemyFlyingComponent;

    private void Start()
    {
        enemyFlyingComponent = this.GetComponentInParent<EnemyFlying>();
        this.GetComponent<BoxCollider>().size = new Vector3(enemyFlyingComponent.dodgeDistance, 350, enemyFlyingComponent.dodgeDistance);
    }

    // If anything eneters the enemy's dodge box, dodge it
    // if player dodge either up or down, slow down to not hit player
    // if anything else, dodge up
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.transform.parent.gameObject.name} collided with {other.gameObject.name}");

        // doge player (dodging the player takes priority)
        if (other.gameObject.transform.parent.tag == "Player")
        {
            enemyFlyingComponent.SetDodging(EDodgeType.Player, other.gameObject);
        }
        // ignore bullets or continue to dodge if already doing so
        //else if (currentlyDodgingObject != null)
        //{
        //    // do nothing
        //}
        // dodge other enemies
        else if (other.gameObject.transform.parent.tag == "Enemy" && !other.isTrigger)
        {
            enemyFlyingComponent.SetDodging(EDodgeType.OtherEnemy, other.gameObject);
        }
        // dodge stationary objects
        else if (!other.isTrigger)
        {
            // dodge anything else
            enemyFlyingComponent.SetDodging(EDodgeType.StationaryObject, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject currentlyDodgingObject = enemyFlyingComponent.GetCurrentlyDodgingObject();
        // only if the object that is currently being dodged leaves the dodging radius (and it still exists, do you stop dodging
        if (currentlyDodgingObject && other.gameObject == currentlyDodgingObject)
        {
            enemyFlyingComponent.SetDodging(EDodgeType.False, null, false);
        }
        if (!currentlyDodgingObject)
        {
            enemyFlyingComponent.SetDodging(EDodgeType.False, null, false);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        try
        {
            // draw enemy dodging distance
            UnityEditor.Handles.color = Color.red;
            Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y + this.GetComponent<BoxCollider>().center.y, this.transform.position.z);
            UnityEditor.Handles.DrawWireCube(center, this.GetComponent<BoxCollider>().size);
        }
        catch
        {

        }
#endif
    }
}
