using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCamTrack : MonoBehaviour
{

    public Transform cylinder;
    public Rigidbody planeRB;

    private Vector3 centerOfImpactAreaPos;

    private BombDropController bdc;

    private float deltaH;
    private float deltaZ;
    private float thetaT;
    private float thetaA;
    private float thetaG;
    private float thetaO;
    private float deltaL;

    private float offsetZ;
    private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        bdc = GameObject.FindObjectOfType<BombDropController>();
    }

    private void FixedUpdate()
    {
        centerOfImpactAreaPos = this.GetComponentInParent<FindImpactCenter>().impactCenterPos;
        this.transform.LookAt(centerOfImpactAreaPos);
        //Debug.Log(new Vector3((int)centerOfImpactAreaPos.x, (int)centerOfImpactAreaPos.y, (int)centerOfImpactAreaPos.z));
        CalcDeltaZ();
        CalcDeltaH();
        CalcThetaT();
        CalcThetaG();
        CalcThetaA();
        CalcThetaO();
        CalcDeltaL();
        CalcOffset();
        Debug.DrawRay(this.transform.position, this.transform.forward * 10000, Color.black);
        cylinder.transform.localPosition = new Vector3(0, 0, deltaL);
        //Debug.Log("Zoff: " + offsetZ.ToString() + "\nYoff: " + offsetY);
        cylinder.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void CalcDeltaZ()
    {
        deltaZ = Mathf.Abs(Vector3.Magnitude(Vector3.Scale(centerOfImpactAreaPos, new Vector3(1, 0, 1))
                          - Vector3.Scale(planeRB.transform.position, new Vector3(1,0,1))));
    }

    private void CalcDeltaH()
    {
        deltaH = Mathf.Abs(Vector3.Magnitude(Vector3.Scale(centerOfImpactAreaPos, new Vector3(0, 1, 0))
                          - Vector3.Scale(planeRB.transform.position, new Vector3(0, 1, 0))) -65);
    }

    private void CalcThetaT()
    {
        thetaT = Mathf.Rad2Deg * Mathf.Atan(deltaZ / deltaH);
    }

    private void CalcThetaG()
    {
        thetaG = 90 - thetaT;
    }

    private void CalcThetaA()
    {
        thetaA = thetaT - Mathf.Rad2Deg * Mathf.Atan((deltaZ - bdc.radiusGround) / deltaH);
    }

    private void CalcThetaO()
    {
        thetaO = 180 - thetaG - thetaA;
    }

    private void CalcDeltaL()
    {
        deltaL = Mathf.Sin(Mathf.Deg2Rad * thetaO) / Mathf.Sin(Mathf.Deg2Rad * thetaA);
    }

    private void CalcOffset()
    {
        offsetZ = Mathf.Sin(Mathf.Deg2Rad * thetaT) * deltaL;
        offsetY = Mathf.Sqrt((deltaL * deltaL) - (offsetZ * offsetZ));
    }

    

}
