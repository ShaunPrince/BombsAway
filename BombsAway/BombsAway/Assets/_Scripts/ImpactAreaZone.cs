using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAreaZone : MonoBehaviour
{

    [SerializeField]
    private BombDropController bdc;
    [SerializeField]
    private BombAreaParent bap;
    private DynamicAltitude da;
    private Rigidbody planeRB;
    public float radius;
    // Start is called before the first frame update
    void Awake()
    {
        da = GameObject.FindObjectOfType<DynamicAltitude>();
        planeRB = this.gameObject.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetHeight();
        SetScale();
        //modelTF.LookAt(planeRB.transform);
        //modelTF.Rotate(-90, 0, 0,Space.Self) ;
        SetOffset();
    }

    private void SetHeight()
    {
        //spotLight.transform.parent.localPosition = new Vector3(0, 0, 0);
        //    Vector3.Scale(planeRB.velocity, new Vector3(1, 0, 1)).magnitude * bdc.timeOfFlight);
    }

    private void SetScale()
    {
        radius = Mathf.Tan(Mathf.Deg2Rad * bdc.theta) * bdc.h1;
        //modelTF.localScale = new Vector3(bdc.r1 * 2 * bdc.timeOfFlight, 5, bdc.r1 * 2 * bdc.timeOfFlight);
        //spotLight.spotAngle = bdc.theta;
    }

    private void SetOffset()
    {
        bap.transform.localPosition = Vector3.forward * Vector3.Magnitude(Vector3.Scale(planeRB.velocity, new Vector3(1, 0, 1))) * bdc.timeOfFlight;
        //bap.transform.Translate(0,0, Vector3.Magnitude(Vector3.Scale(planeRB.velocity, new Vector3(1, 0, 1))) * bdc.timeOfFlight,Space.Self);
    }
}
