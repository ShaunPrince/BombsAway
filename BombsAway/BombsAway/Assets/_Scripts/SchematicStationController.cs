using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchematicStationController : MonoBehaviour
{
    private GameObject schematicCam;
    private GameObject sphereManager;

    private Vector3 camPos;
    private Vector3 managerPos;
    // Start is called before the first frame update
    void Start()
    {
        schematicCam = this.transform.GetChild(0).gameObject;
        camPos = schematicCam.transform.position;
        sphereManager = this.transform.GetChild(2).gameObject;
        managerPos = sphereManager.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        schematicCam.transform.position = camPos;
        sphereManager.transform.position = managerPos;
    }
}
