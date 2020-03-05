using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public static List<Station> stations;
    public static Station currentCenterStation;

    public static ControlScheme currentlyActiveControlScheme;

    public StationDisplayManager centerDisplayController;

    public SchematicSphereManager schematicManager;
    private SetNewCameraListner newListner;
    // Start is called before the first frame update
    void Awake()
    {
        stations = new List<Station>();
        InitializeStationsArray();
        centerDisplayController = GameObject.FindGameObjectWithTag("PlayerUIandCamera").GetComponent<StationDisplayManager>();
        //schematicManager = this.GetComponentInChildren<SchematicSphereManager>();
        newListner = this.GetComponent<SetNewCameraListner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeStationsArray()
    {
        foreach(Station s in this.GetComponentsInChildren<Station>())
        {
            stations.Add(s);
            s.controlScheme.isActiveControlScheme = false;
        }
    }

    public void SetMainStation(EStationID newMainStationID)
    {
        if (newMainStationID == EStationID.None)
        {
            return;
        }
        else
        {
            Station oldCenterStation = currentCenterStation;
            currentCenterStation = stations[(int)newMainStationID];
            newListner.SetNewListner(oldCenterStation, currentCenterStation);
            centerDisplayController.SetMainStation(newMainStationID);
            schematicManager.SetNewActiveStation((int)newMainStationID);
            SetActiveControlScheme(newMainStationID);
        }

    }

    public void SetActiveControlScheme(EStationID newStationID)
    {


        currentlyActiveControlScheme?.SetActiveControl(false);


        //Debug.Log(stations[(int)newStationID].controlScheme);
        currentlyActiveControlScheme = stations[(int)newStationID].controlScheme;
        currentlyActiveControlScheme.SetActiveControl(true);
    }
}
