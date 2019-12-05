using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchematicSphereManager : MonoBehaviour
{
    public GameObject leftGun;
    public GameObject rightGun;
    public GameObject tailGun;

    private List<GameObject> schematicSpheres = new List<GameObject>();
    private int currentStationID;
    private LineRenderer leftGunLR;
    private LineRenderer rightGunLR;
    private LineRenderer tailGunLR;
    // Start is called before the first frame update
    void Start()
    {
        int size = StationManager.stations.Count;
        for (int i = 1; i < size; ++i)
        {
            schematicSpheres.Add(this.transform.GetChild(i-1).gameObject);
            schematicSpheres[i-1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        }
        currentStationID = 0;
        leftGunLR = schematicSpheres[3].GetComponent<LineRenderer>();
        rightGunLR = schematicSpheres[4].GetComponent<LineRenderer>();
        tailGunLR = schematicSpheres[6].GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetLeftGunAim();
    }

    public void SetNewActiveStation(int newStationID)
    {
        schematicSpheres[currentStationID].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        schematicSpheres[newStationID-1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
        currentStationID = newStationID - 1;
    }

    private void SetLeftGunAim()
    {
        GameObject leftGunCircle = schematicSpheres[3];
        Vector3 linePos = new Vector3(leftGunCircle.transform.position.x, leftGunCircle.transform.position.y + 0.01f, leftGunCircle.transform.position.z);

        leftGunLR.SetPosition(0, linePos);
        leftGunLR.SetPosition(1, leftGunCircle.transform.right * -0.5f + linePos);
    }
}
