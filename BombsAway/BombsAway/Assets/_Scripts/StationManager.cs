using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public static List<Station> stations = new List<Station>();
    public static Station currentCenterStation;

    public static ControlScheme currentlyActiveControlScheme;

    public StationDisplayManager centerDisplayController;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeStationsArray();
        centerDisplayController = GameObject.FindGameObjectWithTag("PlayerUIandCamera").GetComponent<StationDisplayManager>();

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
            currentCenterStation = stations[(int)newMainStationID];
            centerDisplayController.SetMainStation(newMainStationID);
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
