using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    public GameObject leftWings;
    public GameObject rightWings;

    // Start is called before the first frame update
    void Start()
    {
        LeftWingOut();
        RightWingOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LeftWingOut()
    {
        iTween.RotateBy(leftWings, iTween.Hash("rotation", new Vector3(0,20f,0),
                                               "time", 1f, "looptype", "pingPong"));
    }
    /*
    private void LeftWingIn()
    {
        iTween.RotateTo(LeftWings, iTween.Hash("rotation", new Vector3(0, -20, 0),
                                               "time", 1f, "oncomplete", "LeftWingOut"));
    }*/

    private void RightWingOut()
    {
        iTween.RotateBy(rightWings, iTween.Hash("rotation", new Vector3(0, -20f, 0),
                                               "time", 1f, "looptype", "pingPong"));
    }
    /*
    private void RightWingIn()
    {
        iTween.RotateTo(LeftWings, iTween.Hash("rotation", new Vector3(0, 20, 0),
                                               "time", 1f, "oncomplete", "RightWingOut"));
    }*/
}
