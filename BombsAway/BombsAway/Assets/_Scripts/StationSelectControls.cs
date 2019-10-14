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
        Cursor.lockState = CursorLockMode.Locked;
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
            return EStationID.Map;
        }
        else
        {
            return EStationID.None;
        }
    }



}
