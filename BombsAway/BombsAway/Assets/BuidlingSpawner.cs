using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawn buildings from a list of buildings
// choose a random x,z point
// find the y position from the terrain at that point
// make sure it is not within a certain radius of any other building

public class BuidlingSpawner : MonoBehaviour
{
    public GameObject[] Buildings;
    // EVENTUALLY GET THIS FROM ONE PLACE
    public float worldCenterX;
    public float worldCenterZ;
    public float worldLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
