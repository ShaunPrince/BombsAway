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
        SetRightGunAim();
        SetTailGunAim();
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
        Vector3 linePos = leftGunCircle.transform.position;
        leftGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -leftGun.transform.localRotation.eulerAngles.y);
        leftGunLR.SetPosition(0, linePos);
        leftGunLR.SetPosition(1, leftGunCircle.transform.right * -0.5f + linePos);
    }

    private void SetRightGunAim()
    {
        GameObject rightGunCircle = schematicSpheres[4];
        Vector3 linePos = rightGunCircle.transform.position;
        rightGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -rightGun.transform.localRotation.eulerAngles.y);
        rightGunLR.SetPosition(0, linePos);
        rightGunLR.SetPosition(1, (rightGunCircle.transform.right * 0.5f) + linePos);
    }

    private void SetTailGunAim()
    {
        GameObject tailGunCircle = schematicSpheres[6];
        Vector3 linePos = tailGunCircle.transform.position;
        tailGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -tailGun.transform.localRotation.eulerAngles.y);
        tailGunLR.SetPosition(0, linePos);
        tailGunLR.SetPosition(1, (tailGunCircle.transform.up * -0.5f) + linePos);
    }
}
