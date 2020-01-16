using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFragmentOnDeath : MonoBehaviour
{
    public float fragForce;
    public List<GameObject> fragments;
    public float timeUntilDespawn;
    private float timeSinceFrag;
    private bool isFragged;
    // Start is called before the first frame update
    void Start()
    {
        isFragged = false;
        if(fragForce == 0)
        {
            fragForce = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isFragged)
        {
            if (timeSinceFrag < timeUntilDespawn)
            {
                timeSinceFrag += Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }

    public void Fragment()
    {

        foreach(GameObject frag in fragments)
        {
            Rigidbody temp = frag.GetComponent<Rigidbody>();
            if (temp != null)
            {
                temp.isKinematic = false;
                temp.AddForce(new Vector3(Random.Range(-fragForce, fragForce), Random.Range(-fragForce, 0), Random.Range(-fragForce, fragForce)),ForceMode.VelocityChange);
                
            }
        }
        isFragged = true;


    }
}
