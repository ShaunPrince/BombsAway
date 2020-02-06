using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewCameraListner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewListner(Station oldStation, Station newStation)
    {
        if (oldStation != null)
        {
            AudioListener oldListner = oldStation.gameObject.GetComponent<AudioListener>();
            if (oldListner != null)
            {
                oldListner.enabled = false;
            }
        }
        AudioListener newListner = newStation.gameObject.GetComponent<AudioListener>();
        if (newListner != null)
        {
            newListner.enabled = true;
        }
    }
}
