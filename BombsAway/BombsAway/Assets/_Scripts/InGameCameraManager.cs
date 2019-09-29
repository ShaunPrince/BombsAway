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
    public RectTransform blackoutPannelRectTran;
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
        SetCameraScales(chosenCamScale);
        //Debug.Log(canvasRectTran.rect.width);
        blackoutPannelRectTran.anchoredPosition = new Vector3(activeCenterCam.rect.x * canvasWidth, activeCenterCam.rect.y * canvasHeight, 0f);
        blackoutPannelRectTran.sizeDelta = new Vector2(activeCenterCam.rect.width * canvasWidth, activeCenterCam.rect.height * canvasHeight);
        //blackoutPannelRectTran.localScale = new Vector3(activeCenterCam.rect.)

        activeCenterCam.rect = new Rect(0, 0, 1, 1);
        activeCenterCam.depth = 0;
    }

    public void SetCameraScales(float newScale)
    {
        foreach (Camera c in cams)
        {
            // calculate the offset based on the chosen cam scale
            c.rect = new Rect((.5f - chosenCamScale/2) * c.GetComponent<InGameCam>().xCord, (.5f - chosenCamScale/2)
                * c.GetComponent<InGameCam>().yCord, chosenCamScale, chosenCamScale);
            c.depth = 1;

        }
    }
}
