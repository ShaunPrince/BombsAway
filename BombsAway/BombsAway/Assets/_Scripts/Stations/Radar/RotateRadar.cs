using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRadar : MonoBehaviour
{
    public float secondsPerRotation;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 curRotation = rb.transform.rotation.eulerAngles;
        Vector3 newRotation = curRotation + new Vector3(0, 360 / (secondsPerRotation * 50), 0);
        rb.MoveRotation(Quaternion.Euler(newRotation));
    }
}
