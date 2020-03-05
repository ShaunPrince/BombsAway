using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMapCompass : MonoBehaviour
{
    public Transform ship;
    private Vector3 oldtransform;
    void Start()
    {
        Vector3 oldtransform = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newrot = new Vector3(0, ship.localEulerAngles.y, 0);
        Vector3 delta = oldtransform - newrot;
        this.transform.localRotation = Quaternion.Euler(0f, ship.localEulerAngles.y, 0f);
        oldtransform = newrot;
    }
}
