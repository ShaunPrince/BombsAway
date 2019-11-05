using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomControls : MonoBehaviour
{
    public StationDisplayPosAndScaleController stationDisplayPosAndScaleController;
    private int chosenScaleIndex;
    // Start is called before the first frame update
    void Awake()
    {
        stationDisplayPosAndScaleController = GameObject.FindObjectOfType<StationDisplayPosAndScaleController>();
        chosenScaleIndex = (int)stationDisplayPosAndScaleController.chosenCenterCamScale;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZoom(CheckForZoomChange());
    }

    public void UpdateZoom(int newZoomIndex)
    {
        stationDisplayPosAndScaleController.chosenCenterCamScale = (ECenterCamScale)newZoomIndex;
    }


    public int CheckForZoomChange()
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
