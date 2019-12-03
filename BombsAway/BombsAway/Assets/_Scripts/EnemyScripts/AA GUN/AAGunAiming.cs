using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunAiming : MonoBehaviour
{
    public Rigidbody targetRB;
    public Transform gunTF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetRB != null)
        {
            gunTF.LookAt(targetRB.transform);
        }

    }
}
