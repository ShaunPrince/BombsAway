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
        Vector3 newrot = new Vector3(0,0, ship.rotation.y);
        Vector3 delta = oldtransform - newrot;
        this.transform.SetPositionAndRotation(this.transform.position, Quaternion.Euler(this.transform.rotation.eulerAngles -delta));
        oldtransform = newrot;
    }
}
