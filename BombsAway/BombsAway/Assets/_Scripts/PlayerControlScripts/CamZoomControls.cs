using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomControls : MonoBehaviour
{
    public InGameCameraManager inGameCamManager;
    public float[] scalePresets;
    public int chosenScaleIndex;
    // Start is called before the first frame update
    void Start()
    {
        chosenScaleIndex = 0;
        scalePresets = new float[3];
        scalePresets[0] = inGameCamManager.evenCamScale;
        scalePresets[1] = inGameCamManager.smallCamScale;
        scalePresets[2] = inGameCamManager.centerCamScale;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZoom(CheckForZoom());
    }

    public void UpdateZoom(int newZoomIndex)
    {
        inGameCamManager.chosenCamScale = scalePresets[newZoomIndex];
        inGameCamManager.UpdateCameras();
    }


    public int CheckForZoom()
    {
        float scrollWheelDelta = Input.mouseScrollDelta.y;
        if(scrollWheelDelta < 0)
        {
            if(chosenScaleIndex <= 0)
            {
                return 0;
            }
            else
            {
                chosenScaleIndex -= 1;
                return chosenScaleIndex;
            }
        }
        else if(scrollWheelDelta > 0)
        {
            if(chosenScaleIndex >= 2)
            {
                return 2;
            }
            else
            {
                chosenScaleIndex += 1;
                return chosenScaleIndex;
            }
        }
        else
        {
            return chosenScaleIndex;
        }
    }

}
