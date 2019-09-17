using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public Camera stationCamera;
    
    public Crewman stationCrewman;

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


    public void UpdateCameraStatus()
    {
        if (stationCrewman == null)
        {
            //Eventually set a UI cover and/or cullingmask when unmanned
            //for now just turn off the camera
            stationCamera.gameObject.SetActive(false);
        }
        else
        {
            stationCamera.gameObject.SetActive(true);
        }
    }
}
