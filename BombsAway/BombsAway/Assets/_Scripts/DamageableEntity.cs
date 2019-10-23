using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    public EAllegiance allegiance;
    public float health;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if(allegianceOfIncomingDamage != allegiance)
        {
            health -= incomingDamage;
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

    }
}
