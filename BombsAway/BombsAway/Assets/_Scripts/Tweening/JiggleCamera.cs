using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleCamera : MonoBehaviour
{
    public GameObject pilotCamera;
    private Flying flyingComponent;
    private PlayerFlightControls flightControlls;
    private bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        flyingComponent = this.GetComponent<Flying>();
        flightControlls = this.GetComponentInChildren<PlayerFlightControls>();
    }

    // Update is called once per frame
    void Update()
    {
        // currently turning
        if (flyingComponent.desiredDir != flyingComponent.currentDir)
        {
            turning = true;
        }
        // stopped turning
        if (turning && flyingComponent.desiredDir == flyingComponent.currentDir)
        {
            if (flightControlls.turningLeft) JiggleCameraLeft();
            if (!flightControlls.turningLeft) JiggleCameraRight();
            turning = false;
        }
    }

    public void JiggleCameraLeft()
    {
        iTween.RotateBy(pilotCamera, iTween.Hash("amount", new Vector3(0, -0.01f, 0),
                                                 "time", 1f, "easetype", "easeOutElastic"));
        //iTween.RotateBy(pilotCamera, iTween.Hash("amount", new Vector3(0, 0.01f, 0),
        //                                         "time", 1f, "easetype", "linear",
        //                                         "delay", .8f));
    }

    public void ReturnCameraLeft()
    {
        iTween.RotateBy(pilotCamera, iTween.Hash("amount", new Vector3(0, .01f, 0),
                                                 "time", .5f, "easetype", "linear"));
    }

    public void JiggleCameraRight()
    {
        iTween.RotateBy(pilotCamera, iTween.Hash("amount", new Vector3(0, 0.01f, 0),
                                                 "time", 1f, "easetype", "easeOutElastic"));
        //iTween.RotateBy(pilotCamera, iTween.Hash("amount", new Vector3(0, 0.01f, 0),
        //                                         "time", 1f, "easetype", "linear",
        //                                         "delay", .8f));
    }
}
