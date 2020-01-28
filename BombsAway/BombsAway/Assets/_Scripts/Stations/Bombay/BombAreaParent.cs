using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAreaParent : MonoBehaviour
{
    public float rotateForce;
    public float radius;
    // Start is called before the first frame update
    void Awake()
    {
        this.transform.localPosition = Vector3.zero;
        this.GetComponent<Rigidbody>().centerOfMass = this.transform.position;
        this.GetComponent<Rigidbody>().AddTorque(0, rotateForce, 0);
        SetNodeDistance();

    }

    // Update is called once per frame
    void Update()
    {
        SetNodeDistance();
    }

    public void SetNodeDistance()
    {
        int numNodes = this.transform.childCount;
        int angleOffset = 0;
        for(int i = 0; i <numNodes;++i)
        {
            this.transform.GetChild(i).localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad*angleOffset)*radius
                , this.transform.GetChild(i).localPosition.y, Mathf.Sin(Mathf.Deg2Rad*angleOffset) *radius);
            angleOffset += 360 / numNodes;
            
        }
    }
}
