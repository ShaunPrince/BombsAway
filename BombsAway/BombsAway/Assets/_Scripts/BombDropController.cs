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

    private float radiusGround;

    [SerializeField]
    private Rigidbody planeRB;
    private DynamicAltitude da;
    // Start is called before the first frame update
    void Awake()
    {
        planeRB = this.GetComponentInParent<Rigidbody>();
        da = GameObject.FindObjectOfType<DynamicAltitude>();
        h1 = da.straitDownAlt;
        CalcTOF(h1);
        CalcRadGround();

    }

    // Update is called once per frame
    void Update()
    {
        h1 = da.straitDownAlt;
        CalcTOF(h1);
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


    private void CalcTOF(float h1)
    {
        timeOfFlight = Mathf.Sqrt((2 * h1) / 9.81f);
    }

    private void CalcRadGround()
    {
        radiusGround = Mathf.Tan(Mathf.Deg2Rad * theta) * h1;
    }
}
