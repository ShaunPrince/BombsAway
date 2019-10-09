using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCameraManager : MonoBehaviour
{
    //   public enum ECamScale { small, even, center }; 
    // public ECamScale chosenCamScale;
    public float chosenCamScale;
    public float smallCamScale;
    public float smallCamOffset;
    public float evenCamScale;
    public float evenCamOffset;
    public float centerCamScale;
    public float centerCamOffset;
    public Camera[] cams;
    public Camera activeCenterCam;
    public Camera schematicCam;
    public RectTransform canvasRectTran;
    private float canvasWidth;
    private float canvasHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasWidth = canvasRectTran.rect.width;
        canvasHeight = canvasRectTran.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        //Set this to only occur when cam change occurs




    }

    public void SetMainCam(Station.EStationID newStationID)
    {
        activeCenterCam = cams[(int)newStationID];
        UpdateCameras();
    }

    public void UpdateCameras()
    {
        foreach (Camera c in cams)
        {
            // calculate the offset based on the chosen cam scale
            c.rect = new Rect((.5f - chosenCamScale/2) * c.GetComponent<InGameCam>().xCord, (.5f - chosenCamScale/2)
                * c.GetComponent<InGameCam>().yCord, chosenCamScale, chosenCamScale);
            c.depth = 2;
        }

        schematicCam.rect = activeCenterCam.rect;

        if (chosenCamScale < .3)
        {
            activeCenterCam.rect = new Rect(0, 0, 1, 1);
        }
        else
        {
            activeCenterCam.rect = new Rect((.5f - chosenCamScale / 2), (.5f - chosenCamScale / 2)
                , chosenCamScale, chosenCamScale);
        }
        activeCenterCam.depth = 0;
    }
}
