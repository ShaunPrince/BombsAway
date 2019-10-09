using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlScheme currentlyActiveControlScheme;

    public ControlScheme[] controlSchemes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveControlScheme(Station.EStationID newStationID)
    {
        if(currentlyActiveControlScheme != null)
        {
            currentlyActiveControlScheme.isActiveControlScheme = false;
            currentlyActiveControlScheme.enabled = false;

        }
        controlSchemes[(int)newStationID].enabled = true;
        controlSchemes[(int)newStationID].isActiveControlScheme = true;
        currentlyActiveControlScheme = controlSchemes[(int)newStationID];
    }
}
