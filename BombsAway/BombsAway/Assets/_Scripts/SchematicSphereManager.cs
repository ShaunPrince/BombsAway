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
    private LineRenderer leftGunLR1;
    private LineRenderer leftGunLR2;
    private LineRenderer rightGunLR1;
    private LineRenderer rightGunLR2;
    private LineRenderer tailGunLR1;
    private LineRenderer tailGunLR2;
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
        leftGunLR1 = schematicSpheres[3].GetComponent<LineRenderer>();
        leftGunLR2 = schematicSpheres[3].transform.GetChild(0).GetComponent<LineRenderer>();
        rightGunLR1 = schematicSpheres[4].GetComponent<LineRenderer>();
        rightGunLR2 = schematicSpheres[4].transform.GetChild(0).GetComponent<LineRenderer>();
        tailGunLR1 = schematicSpheres[6].GetComponent<LineRenderer>();
        tailGunLR2 = schematicSpheres[6].transform.GetChild(0).GetComponent<LineRenderer>();
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
        GameObject leftGunCircle1 = schematicSpheres[3];
        GameObject leftGunCircle2 = leftGunCircle1.transform.GetChild(0).gameObject;
        Vector3 linePos1 = leftGunCircle1.transform.position;
        Vector3 linePos2 = leftGunCircle2.transform.position;
        leftGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -leftGun.transform.localRotation.eulerAngles.y - 30.0f);
        leftGunCircle2.transform.localRotation = Quaternion.identity;
        leftGunCircle2.transform.Rotate(0.0f, 0.0f, 60.0f);
        leftGunLR1.SetPosition(0, linePos1);
        leftGunLR1.SetPosition(1, leftGunCircle1.transform.right * -0.5f + linePos1);
        leftGunLR2.SetPosition(0, linePos2);
        leftGunLR2.SetPosition(1, leftGunCircle2.transform.right * -0.5f + linePos2);
    }

    private void SetRightGunAim()
    {
        GameObject rightGunCircle1 = schematicSpheres[4];
        GameObject rightGunCircle2 = rightGunCircle1.transform.GetChild(0).gameObject;
        Vector3 linePos1 = rightGunCircle1.transform.position;
        Vector3 linePos2 = rightGunCircle2.transform.position;
        rightGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -rightGun.transform.localRotation.eulerAngles.y - 30.0f);
        rightGunCircle2.transform.localRotation = Quaternion.identity;
        rightGunCircle2.transform.Rotate(0.0f, 0.0f, 60.0f);
        rightGunLR1.SetPosition(0, linePos1);
        rightGunLR1.SetPosition(1, (rightGunCircle1.transform.right * 0.5f) + linePos1);
        rightGunLR2.SetPosition(0, linePos2);
        rightGunLR2.SetPosition(1, (rightGunCircle2.transform.right * 0.5f) + linePos2);
    }

    private void SetTailGunAim()
    {
        GameObject tailGunCircle1 = schematicSpheres[6];
        GameObject tailGunCircle2 = tailGunCircle1.transform.GetChild(0).gameObject;
        Vector3 linePos1 = tailGunCircle1.transform.position;
        Vector3 linePos2 = tailGunCircle2.transform.position;
        tailGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -tailGun.transform.localRotation.eulerAngles.y - 30.0f);
        tailGunCircle2.transform.localRotation = Quaternion.identity;
        tailGunCircle2.transform.Rotate(0.0f, 0.0f, 60.0f);
        tailGunLR1.SetPosition(0, linePos1);
        tailGunLR1.SetPosition(1, (tailGunCircle1.transform.up * -0.5f) + linePos1);
        tailGunLR2.SetPosition(0, linePos2);
        tailGunLR2.SetPosition(1, (tailGunCircle2.transform.up * -0.5f) + linePos2);
    }
}
