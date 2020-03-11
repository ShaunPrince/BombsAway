using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBombRadius : MonoBehaviour
{
    private BombDropController bdc;
    private BombController[] bombs;
    private PlayerFlightControls pfc;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        bdc = GameObject.FindObjectOfType<BombDropController>();
        pfc = GameObject.FindObjectOfType<PlayerFlightControls>();
        height = pfc.presetAlts[0];
    }

    // Update is called once per frame
    void Update()
    {
        bombs = GameObject.FindObjectsOfType<BombController>();
        foreach(BombController bc in bombs)
        {
            if(bc.isDropping == false)
            {
                bc.gameObject.GetComponent<SphereCollider>().radius = Mathf.Tan(Mathf.Deg2Rad * bdc.theta) * height * 2;
            }
        }
    }
}
