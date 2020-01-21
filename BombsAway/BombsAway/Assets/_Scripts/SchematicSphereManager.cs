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
    private bool stationsSet;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        int size = 9;
        for (int i = 1; i < size; ++i)
        {
            schematicSpheres.Add(this.transform.GetChild(i - 1).gameObject);
            schematicSpheres[i - 1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        }
        stationsSet = true;
        currentStationID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stationsSet)
        {
            SetLeftGunAim();
            SetRightGunAim();
            SetTailGunAim();
        }
    }

    public void SetNewActiveStation(int newStationID)
    {
        if (stationsSet)
        {
            schematicSpheres[currentStationID].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            schematicSpheres[newStationID - 1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
            currentStationID = newStationID - 1;
        }
    }

    private void SetLeftGunAim()
    {
        GameObject leftGunCircle = schematicSpheres[3];
        GameObject cone = leftGunCircle.transform.GetChild(0).gameObject;
        leftGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -leftGun.transform.localRotation.eulerAngles.y);
    }

    private void SetRightGunAim()
    {
        GameObject rightGunCircle = schematicSpheres[4];
        GameObject cone = rightGunCircle.transform.GetChild(0).gameObject;
        rightGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -rightGun.transform.localRotation.eulerAngles.y);
    }

    private void SetTailGunAim()
    {
        GameObject tailGunCircle = schematicSpheres[6];
        GameObject cone = tailGunCircle.transform.GetChild(0).gameObject;
        tailGunCircle.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -tailGun.transform.localRotation.eulerAngles.y);
        /*
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
        */
    }
}
