using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBoat : MonoBehaviour
{
    public GameObject boat;
    // Start is called before the first frame update
    void Start()
    {
        iTween.RotateBy(boat, iTween.Hash("amount", new Vector3(0, -.03f, 0),
                                               "time", 6f, "easetype", "linear"));
        RockLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RockLeft()
    {
        iTween.RotateBy(boat, iTween.Hash("amount", new Vector3(0, .06f, 0),
                                               "time", 6f, "easetype", "linear",
                                               "looptype", "pingPong"));
    }

    private void RockRight()
    {
        iTween.RotateBy(boat, iTween.Hash("amount", new Vector3(0, -.1f, 0),
                                               "time", 6f, "easetype", "linear",
                                               "oncomplete", "RockLeft"));
    }
}
