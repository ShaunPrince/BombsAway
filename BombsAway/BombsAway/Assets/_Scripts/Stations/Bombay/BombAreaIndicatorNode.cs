using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAreaIndicatorNode : MonoBehaviour
{
    public LayerMask lm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAlt();
    }

    private void SetAlt()
    {
        if(Physics.Raycast(this.transform.position + new Vector3(0,1000,0),Vector3.down,out RaycastHit hit, 100000f, lm))
        {
            this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z);
        }
    }
}
