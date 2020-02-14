using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropController : MonoBehaviour
{

    public float h1;
    public float theta;

    public float timeOfFlight;

    private float xOffsetForce;
    private float zOffsetForce;

    public float radiusGround;

    private FindImpactCenter findImpactCent;

    [SerializeField]
    private Rigidbody planeRB;

    // Start is called before the first frame update
    void Awake()
    {
        planeRB = this.GetComponentInParent<Rigidbody>();
        findImpactCent = this.GetComponent<FindImpactCenter>();



        // h1 = da.straitDownAlt;
        CalcH1();
        CalcTOF();
        CalcRadGround();

    }

    // Update is called once per frame
    void Update()
    {
        CalcH1();
        CalcTOF();
        CalcRadGround();
    }

    public void Drop(GameObject bomb)
    {
        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        bombRB.isKinematic = false;
        bombRB.useGravity = true;
        bombRB.velocity = Vector3.Scale(planeRB.velocity, (new Vector3(1, 0, 1)));
        if(planeRB.velocity.y < 0)
        {
            bombRB.velocity = new Vector3(bombRB.velocity.x, planeRB.velocity.y - 1, bombRB.velocity.z);
        }
        Vector2 circVect = Random.insideUnitCircle * (radiusGround / timeOfFlight);
        bombRB.AddForce(circVect.x, 0, circVect.y, ForceMode.VelocityChange);
        bomb.GetComponent<BombController>().SetToDrop();
    }


    private void CalcH1()
    {
        h1 = Mathf.Abs(planeRB.transform.position.y - 65 - findImpactCent.impactCenterPos.y);
        //Debug.Log(h1);
    }

    private void CalcTOF()
    {
        timeOfFlight = Mathf.Sqrt(Mathf.Abs((2 * h1) / Physics.gravity.y));
    }

    private void CalcRadGround()
    {
        radiusGround = Mathf.Tan(Mathf.Deg2Rad * theta) * h1;
    }
}
