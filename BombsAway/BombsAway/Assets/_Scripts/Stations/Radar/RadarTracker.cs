using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTracker : MonoBehaviour
{
    private float radius;
    public GameObject PingManager;

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

        //The values printed below should be used for drawing the radar blips
        float rotation = Flying.ConvertToPos360Dir(Vector3.SignedAngle(this.transform.forward, enemyTransform.position - this.transform.position, Vector3.up));
        float dist = Vector3.Distance(this.transform.position, enemyTransform.position) / radius;

        //Debug.Log(this.transform.forward);
        //Debug.Log(rotation);
        //Debug.Log(dist);
        PingManager.GetComponent<PingSpawner>().SpawnPing(dist, rotation);
    }
}
