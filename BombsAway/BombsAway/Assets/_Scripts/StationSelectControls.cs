using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSelectControls : MonoBehaviour
{
    
    public Station[] stations;

    public Station currentSelectedStation;

    public InGameCameraManager inGameCameraManager;

    public ControlsManager controlsManager;

    public Station.EStationID initialMainStation;

    // Start is called before the first frame update
    void Start()
    {
        ApplyStationChange(initialMainStation);
    }

    // Update is called once per frame
    void Update()
    {
        Station.EStationID newStationID = CheckInputForStationChange();
        ApplyStationChange(newStationID);
    }

    //This can be adjusted for radial input
    public Station.EStationID CheckInputForStationChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return Station.EStationID.Schematic;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return Station.EStationID.Repair;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return Station.EStationID.Pilot;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return Station.EStationID.Radar;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return Station.EStationID.LGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return Station.EStationID.RGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return Station.EStationID.BombBay;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return Station.EStationID.TGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return Station.EStationID.Map;
        }
        else
        {
            return Station.EStationID.None;
        }
    }

    public void ApplyStationChange(Station.EStationID newMainStationID)
    {
        if(newMainStationID == Station.EStationID.None)
        {
            return;
        }
        else
        {
            SetMainStation(newMainStationID);
        }
    }

    public void SetMainStation(Station.EStationID newMainStationID)
    {
        if(newMainStationID == Station.EStationID.None)
        {
            return;
        }
        else
        {
            currentSelectedStation = stations[(int)newMainStationID];
            inGameCameraManager.SetMainCam(newMainStationID);
            controlsManager.SetActiveControlScheme(newMainStationID);
        }

    }
}
