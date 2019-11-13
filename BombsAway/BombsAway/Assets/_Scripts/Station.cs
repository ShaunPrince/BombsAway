using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    //THIS IS THE ORDER/ID enum/num FOR THEIR RESPECTIVE STATION IN ANY AND ALL ARRAYS
    public EStationID stationID;

    public Camera stationCamera;

    public ControlScheme controlScheme;
    //public Crewman stationCrewman;


    // Start is called before the first frame update
    void Start()
    {
        stationCamera = this.GetComponentInChildren<Camera>();
        UpdateCameraStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void UpdateCrewman(Crewman newCrewman)
    //{
    //    this.stationCrewman = newCrewman;
    //    UpdateCameraStatus();
    //}

    //public bool IsManned()
    //{
    //    if(stationCrewman == null)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    public void UpdateCameraStatus()
    {
        stationCamera.gameObject.SetActive(true);
    }


}
