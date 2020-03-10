using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutopilot : MonoBehaviour
{

    public float desiredAlt;
    public float desiredSpeed;

    private DynamicAltitude da;
    public Flying fly;
    // Start is called before the first frame update
    void Awake()
    {
        da = this.gameObject.GetComponentInParent<DynamicAltitude>();
        fly = GameObject.FindGameObjectWithTag("PilotStation").GetComponent<Flying>();
    }

    // Update is called once per frame
    void Update()
    {
        fly.SetDesAlt(da.calcStraightDownAlt(desiredAlt));
        fly.SetDesSpeed(desiredSpeed);
    }
}
