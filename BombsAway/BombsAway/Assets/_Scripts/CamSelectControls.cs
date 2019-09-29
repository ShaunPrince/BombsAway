using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSelectControls : MonoBehaviour
{
    public InGameCameraManager inGameCamManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMainCam( CheckInputForCamControl());

    }

    public void UpdateMainCam(int newCamIndex)
    {
        //reject invalid indicies
        if(newCamIndex < 0 || newCamIndex >= inGameCamManager.cams.Length)
        {
            return;
        }
        else
        {
            //on valid index, set main cam
            inGameCamManager.activeCenterCam = inGameCamManager.cams[newCamIndex];
        }
    }  

    //This can be adjusted for radial input
    public int CheckInputForCamControl()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 8;
        }
        else
        {
            return -1;
        }
    }
}
