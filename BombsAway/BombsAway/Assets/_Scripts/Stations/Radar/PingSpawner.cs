using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingSpawner : MonoBehaviour
{

    public GameObject Ping;
    public RadarProperties rp;

    // Start is called before the first frame update
    void Awake()
    {
        rp = GameObject.FindObjectOfType<RadarProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPing(float dist, float rot) {
        
        Transform pingt = this.transform;
        GameObject temp = GameObject.Instantiate(Ping, pingt);
        PingPosition tempPingPosition = temp.GetComponent<PingPosition>();
        tempPingPosition.pngRT = temp.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        tempPingPosition.PositionPNG(dist, rp.radius);
        tempPingPosition.RotatePNGParent(rot);



    }
}
