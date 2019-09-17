using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crewman : MonoBehaviour
{

    public string cmName;
    public float cmHealth;

    public Station startingStation;

    public Station currentStation;

    public Station newStationTest;

    //Stats

    //\Stats

    // Start is called before the first frame update
    void Start()
    {
        MoveToStation(startingStation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RAWR()
    {
        MoveToStation(newStationTest);
        return;
    }


    public bool MoveToStation(Station newStation)
    {
        if(newStation.stationCrewman == null)
        {
            if(this.currentStation != null)
            {
                this.currentStation.UpdateCrewman(null);
            }
            this.currentStation = newStation;
            newStation.UpdateCrewman(this);


            //Eventually add a delay here to simulate the time it takes
            //  to move stations

            //return true if the assignment was successful
            return true;

        }
        else
        {
            //return false if unable to move to the new station
            return false;
        }

    }
}
