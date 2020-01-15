using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public GameObject propeller;

    // Start is called before the first frame update
    void Start()
    {
        Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Rotate()
    {
        iTween.RotateBy(propeller, iTween.Hash("amount", new Vector3(0, 0, 1),
                                               "time", .2f, "easetype", "linear",
                                               "looptype", "loop"));
    }
}
