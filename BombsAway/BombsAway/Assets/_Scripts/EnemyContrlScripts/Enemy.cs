using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
scrip takes care of the randomness - so we dont have to pass variables
simple enemy command script

random spawn
altitude height of player
direction to player from enemy - const update
desired speed (random)

gets to close? go over and fly away and turn around a little bit later to re-attack

damageable entity - script
take damage - health stuffs

just fly towards player for now

use shaun's script - flying script

*/

public class Enemy : DamageableEntity
{
    public int speed;
    [Range(0, 1)]
    public float percision; // in terms of flying towards player, altitude relative to player
                            // don't want perfect aim

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // constantly update location to which to fly to (location of player)
        // constantly update altitude until it is within a certain range of the player?
        // when enemy gets too close to player, raise up, fly over, fly forward for a short time, then return to attacking

    }
}
