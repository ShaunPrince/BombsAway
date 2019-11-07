using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTracker : MonoBehaviour
{
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<RadarProperties>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enemySpotted(Transform enemyTransform)
    {
        //Debug.Log("Spotted: " + enemyTransform.gameObject.name.ToString());
        Debug.Log(Flying.ConvertToPos360Dir(Vector3.SignedAngle(this.transform.forward, enemyTransform.position,Vector3.up)));
        Debug.Log(Vector3.Distance(this.transform.position, enemyTransform.position) / radius);
    }
}
