using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public static List<Station> stations = new List<Station>();
    public Station currentSelectedStation;

    public static ControlScheme currentlyActiveControlScheme;

    public InGameCameraManager inGameCameraManager;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeStationsArray();

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
            currentSelectedStation = stations[(int)newMainStationID];
            inGameCameraManager.SetMainCam(newMainStationID);
            SetActiveControlScheme(newMainStationID);
        }

    }

    public void SetActiveControlScheme(EStationID newStationID)
    {

        if (currentlyActiveControlScheme != null)
        {
            currentlyActiveControlScheme.SetAsInactive();
        }

        //Debug.Log(stations[(int)newStationID].controlScheme);
        currentlyActiveControlScheme = stations[(int)newStationID].controlScheme;
        currentlyActiveControlScheme.SetAsActiveControl();
    }
}
