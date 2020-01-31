using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCamTrack : MonoBehaviour
{
    public Transform impactSpotTF;
    public LayerMask layersToHit;
    public BombDropController bdc;
    public Transform cylinder;
    public ImpactAreaZone IAZ;

    private Vector3 centerOfImpactAreaPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(impactSpotTF.position,Vector3.down, out hit, 10000f,layersToHit))
        {
            centerOfImpactAreaPos = hit.point;
            //Debug.DrawLine(this.transform.position , centerOfImpactAreaPos);

            float theta1; //= Vector3.Angle(-this.transform.parent.up * 100000, hit.point);
            //Debug.DrawRay(this.transform.position, -this.transform.parent.up * 100,Color.green);
            float theta2;// = Vector3.Angle(-this.transform.parent.up * 100000, hit.point + this.transform.parent.TransformVector(new Vector3(0, 0, -IAZ.radius)));
            //Debug.Log("tvect: " +this.transform.parent.TransformVector(0, 0, -bap.radius));
            //Debug.Log("hit+-tvect: " + (hit.point + this.transform.parent.TransformVector(0,0, -bap.radius)));
            //Debug.DrawLine(this.transform.parent.position, hit.point + this.transform.parent.TransformVector(0, 0, -bap.radius), Color.blue);

            float x1 = Vector3.Magnitude(new Vector3(this.transform.parent.position.x, hit.point.y, this.transform.parent.position.z)
                        - hit.point);
            float x2 = x1 - IAZ.radius;
            if(x1 > .01f)
            {
                float y1 = this.transform.position.y - hit.point.y;
                theta1 = Mathf.Atan(x1 / y1);
                theta2 = Mathf.Atan(x2 / y1);
                float y2 = 1 / (Mathf.Tan(theta1) - Mathf.Tan(theta2));
                //Debug.Log(hit.point);
                //Debug.Log("Radius: " + IAZ.radius);
                //Debug.Log("x1: " + x1);
                //Debug.Log("Theta 1: " + theta1);
                //Debug.Log(Mathf.Rad2Deg * Mathf.Atan(x1 / y1));
                //Debug.Log("Theta 2: " + theta2);
                //Debug.Log("y1: " + y1);
                //Debug.Log("y2: " + y2);
                cylinder.transform.localPosition = Vector3.zero;
                cylinder.transform.localPosition = new Vector3(0, -y2, (Mathf.Tan(theta2) * y2) + 1);
                this.gameObject.transform.LookAt(centerOfImpactAreaPos);
            }

        }

    }
}
