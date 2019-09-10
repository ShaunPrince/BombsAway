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
    public float CenterCamScale;
    public float CenterCamOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraScales(chosenCamScale);
    }

    public void SetCameraScales(float newScale)
    {
        foreach (Camera c in this.gameObject.transform.GetComponentsInChildren<Camera>())
        {
            // calculate the offset based on the chosen cam scale
            c.rect = new Rect((.5f - chosenCamScale/2) * c.GetComponent<InGameCam>().xCord, (.5f - chosenCamScale/2)
                * c.GetComponent<InGameCam>().yCord, chosenCamScale, chosenCamScale);

        }
    }
}
