using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropController : MonoBehaviour
{

    private float h1;
    public float theta;
    public float r1;
    public float timeOfFlight;

    private float xOffsetForce;
    private float zOffsetForce;

    [SerializeField]
    private Rigidbody planeRB;
    private DynamicAltitude da;
    // Start is called before the first frame update
    void Awake()
    {
        planeRB = this.GetComponentInParent<Rigidbody>();
        da = GameObject.FindObjectOfType<DynamicAltitude>();
    }

    // Update is called once per frame
    void Update()
    {
        h1 = da.straitDownAlt;
        SetR1();
        CalcTOF(h1);
        CalcX(r1);
        CalcZ(xOffsetForce, r1);
        
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
        bombRB.AddForce(xOffsetForce, 0, zOffsetForce, ForceMode.VelocityChange);
        bomb.GetComponent<BombController>().SetToDrop();
    }

    private void SetR1()
    {
        r1 = Mathf.Tan(Mathf.Deg2Rad * theta) * timeOfFlight;
    }

    private void CalcTOF(float h1)
    {
        timeOfFlight = Mathf.Sqrt((2 * h1) / 9.81f);
    }

    private void CalcX(float r1)
    {
        xOffsetForce = Random.Range(-r1, r1);
    }

    private void CalcZ(float x, float r1)
    {
        float temp;
        temp = (Mathf.Sqrt((r1 * r1) - (x * x)));
        zOffsetForce = Random.Range(-temp, temp);

    }
}
