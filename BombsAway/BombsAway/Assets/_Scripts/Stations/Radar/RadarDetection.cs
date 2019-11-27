using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetection : MonoBehaviour
{
    private RadarTracker rt;


    // Start is called before the first frame update
    void Awake()
    {
        rt = this.GetComponentInParent<RadarTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy") && other.gameObject.TryGetComponent<DamageableEntity>(out otherDE))
        //{ 

            rt.enemySpotted(other.transform);

    }
}
