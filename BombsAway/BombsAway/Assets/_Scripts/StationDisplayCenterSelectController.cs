using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationDisplayCenterSelectController : MonoBehaviour
{
    private StationDisplayManager sdm;
    private Texture schematicTexture;
    // Start is called before the first frame update
    void Start()
    {
        sdm = this.GetComponentInParent < StationDisplayManager > ();

        schematicTexture = StationManager.stations[(int)EStationID.Schematic].stationCamera.targetTexture;
        
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
