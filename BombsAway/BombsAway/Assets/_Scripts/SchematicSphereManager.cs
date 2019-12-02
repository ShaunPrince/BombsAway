using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchematicSphereManager : MonoBehaviour
{
    public Material activeColor;
    public Material inactiveColor;

    private List<GameObject> schematicSpheres = new List<GameObject>();
    private int currentStationID;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewActiveStation(int newStationID)
    {
        //GameObject currentActiveSphere = schematicSpheres[currentStationID];
        //GameObject newActiveSphere = schematicSpheres[newStationID-1];
        schematicSpheres[currentStationID].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        schematicSpheres[newStationID-1].GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
        currentStationID = newStationID - 1;
    }
}
