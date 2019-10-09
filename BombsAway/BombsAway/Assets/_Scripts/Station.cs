using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public enum EStationID { Schematic, Pilot, LGun, RGun, BombBay, TGun, Map, Radar, Repair, None };
    
    public Camera stationCamera;
    
    public Crewman stationCrewman;

    public ControlScheme controlScheme;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCrewman(Crewman newCrewman)
    {
        this.stationCrewman = newCrewman;
        UpdateCameraStatus();
    }

    public bool IsManned()
    {
        if(stationCrewman == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UpdateCameraStatus()
    {
        if (stationCrewman == null)
        {
            //Eventually set a UI cover and/or cullingmask when unmanned
            //for now just turn off the camera

            //Dissabled until controls are finalized 
            //stationCamera.gameObject.SetActive(false);
        }
        else
        {
            stationCamera.gameObject.SetActive(true);
        }
    }

    
}
