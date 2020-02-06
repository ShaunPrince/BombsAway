using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSelectControls : MonoBehaviour
{


    public StationManager sm;

    public EStationID initialMainStation;

    Dictionary<int, int> clockwiseOrderDictClockToNum;
    Dictionary<int, int> clockwiseOrderDictNumToClock;



    // Start is called before the first frame update
    void Start()
    {
        InitializeClockwiseDictClockToNum();
        InitializeClockwiseDictNumToClock();
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
            return EStationID.Map;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return EStationID.Pilot;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
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
            return EStationID.Repair;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            return PrevStation(StationManager.currentCenterStation.stationID);
        }
        else if(Input.GetKeyDown(KeyCode.E))
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
        int currentStation = clockwiseOrderDictClockToNum[(int)currentStationID];
        int newStation = currentStation - 1;
        //check to see if we need to loop back to the start of the enum
        if (newStation <= 0 )
        {
            newStation = (System.Enum.GetNames(typeof(EStationID))).Length - 2;
        }
        return ((EStationID)clockwiseOrderDictNumToClock[newStation]);
    }

    private EStationID NextStation(EStationID currentStationID)
    {
        int currentStation = clockwiseOrderDictClockToNum[(int)currentStationID];
        int newStation = currentStation + 1;
        //check to see if we need to loop back to the start of the enum
        if(newStation == (System.Enum.GetNames(typeof(EStationID))).Length - 1)
        {
            newStation = 1;
        }
        return ((EStationID)clockwiseOrderDictNumToClock[newStation]);
    }

    private void InitializeClockwiseDictNumToClock()
    {
        clockwiseOrderDictNumToClock = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 5 },
            { 5, 8 },
            { 6, 7 },
            { 7, 6 },
            { 8, 4 }
        };
    }

    private void InitializeClockwiseDictClockToNum()
    {
        clockwiseOrderDictClockToNum = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 8 },
            { 5, 4 },
            { 6, 7 },
            { 7, 6 },
            { 8, 5 }
        };
    }



}
