using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingSpawner : MonoBehaviour
{

    public GameObject Ping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPing(float dist, float rot) {
        
        Transform pingt = this.transform;
        pingt.Rotate(0, 0, rot);
        Instantiate(Ping, pingt).transform.GetChild(0).gameObject.transform.position =
            new Vector3(pingt.position.x, pingt.position.y + dist, pingt.position.z);


    }
}
