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
    private LineRenderer leftGunLR3;
    private LineRenderer rightGunLR1;
    private LineRenderer rightGunLR2;
    private LineRenderer rightGunLR3;
    private LineRenderer tailGunLR1;
    private LineRenderer tailGunLR2;
    private LineRenderer tailGunLR3;
    // Start is called before the first frame update
    void Start()
    {
        //int size = StationManager.stations.Count;
        //for (int i = 1; i < size; ++i)
        //{
        //    schematicSpheres.Add(this.transform.GetChild(i-1).gameObject);
        //    schematicSpheres[i-1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        //    SetGunnerChildSphereColor(i - 1, Color.red);
        //}
        currentStationID = 0;
        leftGunLR1 = schematicSpheres[3].GetComponent<LineRenderer>();
        leftGunLR2 = schematicSpheres[3].transform.GetChild(0).GetComponent<LineRenderer>();
        leftGunLR3 = schematicSpheres[3].transform.GetChild(1).GetComponent<LineRenderer>();
        rightGunLR1 = schematicSpheres[4].GetComponent<LineRenderer>();
        rightGunLR2 = schematicSpheres[4].transform.GetChild(0).GetComponent<LineRenderer>();
        rightGunLR3 = schematicSpheres[4].transform.GetChild(1).GetComponent<LineRenderer>();
        tailGunLR1 = schematicSpheres[6].GetComponent<LineRenderer>();
        tailGunLR2 = schematicSpheres[6].transform.GetChild(0).GetComponent<LineRenderer>();
        tailGunLR3 = schematicSpheres[6].transform.GetChild(1).GetComponent<LineRenderer>();
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
        int size = StationManager.stations.Count;
        for (int i = 1; i < size; ++i)
        {
            schematicSpheres.Add(this.transform.GetChild(i - 1).gameObject);
            schematicSpheres[i - 1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            SetGunnerChildSphereColor(i - 1, Color.red);
        }

        schematicSpheres[currentStationID].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        SetGunnerChildSphereColor(currentStationID, Color.red);
        schematicSpheres[newStationID-1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
        SetGunnerChildSphereColor(newStationID - 1, Color.green);
        currentStationID = newStationID - 1;
    }

    private void SetLeftGunAim()
    {
        GameObject leftGunCircle1 = schematicSpheres[3];
        GameObject leftGunCircle2 = leftGunCircle1.transform.GetChild(0).gameObject;
        GameObject leftGunCircle3 = leftGunCircle1.transform.GetChild(1).gameObject;
        Vector3 linePos1 = leftGunCircle1.transform.position;
        Vector3 linePos2 = leftGunCircle2.transform.position;
        Vector3 linePos3 = leftGunCircle3.transform.position;
        leftGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -leftGun.transform.localRotation.eulerAngles.y);
        leftGunCircle2.transform.localRotation = Quaternion.identity;
        leftGunCircle2.transform.Rotate(0.0f, 0.0f, 30.0f);
        leftGunCircle3.transform.localRotation = Quaternion.identity;
        leftGunCircle3.transform.Rotate(0.0f, 0.0f, -30.0f);
        leftGunLR1.SetPosition(0, linePos1);
        leftGunLR1.SetPosition(1, leftGunCircle1.transform.right * -0.5f + linePos1);
        leftGunLR2.SetPosition(0, linePos2);
        leftGunLR2.SetPosition(1, leftGunCircle2.transform.right * -0.5f + linePos2);
        leftGunLR3.SetPosition(0, linePos3);
        leftGunLR3.SetPosition(1, leftGunCircle3.transform.right * -0.5f + linePos3);
    }

    private void SetRightGunAim()
    {
        GameObject rightGunCircle1 = schematicSpheres[4];
        GameObject rightGunCircle2 = rightGunCircle1.transform.GetChild(0).gameObject;
        GameObject rightGunCircle3 = rightGunCircle1.transform.GetChild(1).gameObject;
        Vector3 linePos1 = rightGunCircle1.transform.position;
        Vector3 linePos2 = rightGunCircle2.transform.position;
        Vector3 linePos3 = rightGunCircle3.transform.position;
        rightGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -rightGun.transform.localRotation.eulerAngles.y);
        rightGunCircle2.transform.localRotation = Quaternion.identity;
        rightGunCircle2.transform.Rotate(0.0f, 0.0f, 30.0f);
        rightGunCircle3.transform.localRotation = Quaternion.identity;
        rightGunCircle3.transform.Rotate(0.0f, 0.0f, -30.0f);
        rightGunLR1.SetPosition(0, linePos1);
        rightGunLR1.SetPosition(1, (rightGunCircle1.transform.right * 0.5f) + linePos1);
        rightGunLR2.SetPosition(0, linePos2);
        rightGunLR2.SetPosition(1, (rightGunCircle2.transform.right * 0.5f) + linePos2);
        rightGunLR3.SetPosition(0, linePos3);
        rightGunLR3.SetPosition(1, (rightGunCircle3.transform.right * 0.5f) + linePos3);
    }

    private void SetTailGunAim()
    {
        GameObject tailGunCircle1 = schematicSpheres[6];
        GameObject tailGunCircle2 = tailGunCircle1.transform.GetChild(0).gameObject;
        GameObject tailGunCircle3 = tailGunCircle1.transform.GetChild(1).gameObject;
        Vector3 linePos1 = tailGunCircle1.transform.position;
        Vector3 linePos2 = tailGunCircle2.transform.position;
        Vector3 linePos3 = tailGunCircle3.transform.position;
        tailGunCircle1.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -tailGun.transform.localRotation.eulerAngles.y);
        tailGunCircle2.transform.localRotation = Quaternion.identity;
        tailGunCircle2.transform.Rotate(0.0f, 0.0f, 30.0f);
        tailGunCircle3.transform.localRotation = Quaternion.identity;
        tailGunCircle3.transform.Rotate(0.0f, 0.0f, -30.0f);
        tailGunLR1.SetPosition(0, linePos1);
        tailGunLR1.SetPosition(1, (tailGunCircle1.transform.up * -0.5f) + linePos1);
        tailGunLR2.SetPosition(0, linePos2);
        tailGunLR2.SetPosition(1, (tailGunCircle2.transform.up * -0.5f) + linePos2);
        tailGunLR3.SetPosition(0, linePos3);
        tailGunLR3.SetPosition(1, (tailGunCircle3.transform.up * -0.5f) + linePos3);
    }

    private void SetGunnerChildSphereColor(int index, Color color)
    {
        if (index == 3 || index == 4 || index == 6)
        {
            schematicSpheres[index].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_BaseColor", color);
            schematicSpheres[index].transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_BaseColor", color);
        }
    }
}
