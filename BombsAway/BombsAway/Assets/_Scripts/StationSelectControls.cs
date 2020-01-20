using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSelectControls : MonoBehaviour
{


    public StationManager sm;

    public EStationID initialMainStation;



    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        sm.SetMainStation(initialMainStation);
    }

    // Update is called once per frame
    void Update()
    {
        EStationID newStationID = CheckInputForStationChange();
        sm.SetMainStation(newStationID);
    }

    //This can be adjusted for radial input
    public EStationID CheckInputForStationChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return EStationID.Schematic;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return EStationID.Repair;
        }
        else if (Input.GetKeyDown(KeyCode.Backslash))
        {
            return EStationID.Pilot;
        }
        else if (Input.GetKeyDown(KeyCode.Slash))
        {
            return EStationID.Radar;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return EStationID.LGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return EStationID.RGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return EStationID.BombBay;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return EStationID.TGun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return EStationID.Map;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            return PrevStation(StationManager.currentCenterStation.stationID);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            return NextStation(StationManager.currentCenterStation.stationID);
        }
        else
        {
            return EStationID.None;
        }
    }

    private EStationID PrevStation(EStationID currentStationID)
    {
        int currentStation = (int)currentStationID;
        int newStation = currentStation - 1;
        //check to see if we need to loop back to the start of the enum
        if (newStation < 0 )
        {
            newStation = (System.Enum.GetNames(typeof(EStationID))).Length - 2;
        }
        return ((EStationID)newStation);
    }

    private EStationID NextStation(EStationID currentStationID)
    {
        int currentStation = (int)currentStationID;
        int newStation = currentStation + 1;
        //check to see if we need to loop back to the start of the enum
        if(newStation == (System.Enum.GetNames(typeof(EStationID))).Length)
        {
            newStation = 0;
        }
        return ((EStationID)newStation);
    }



}
