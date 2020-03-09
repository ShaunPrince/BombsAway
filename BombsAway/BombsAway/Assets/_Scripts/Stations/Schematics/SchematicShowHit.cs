using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchematicShowHit : MonoBehaviour
{
    public BoxCollider playerBoxCol;
    public GameObject[] damageArrows;

    //divisions of the rectangle
    public float xL;
    public float xR;
    public float zT;
    public float zB;

    private int hitIndex;
    // Start is called before the first frame update
    void Start()
    {
        hitIndex = -1;
        xL = -playerBoxCol.size.x / 4;
        xR = playerBoxCol.size.x / 4;
        zT = playerBoxCol.size.z / 4;
        zB = -playerBoxCol.size.z / 4;

    }

    // Update is called once per frame
    void Update()
    {
        //hitIndex = 0;
        //damageArrows[hitIndex].GetComponent<Animator>().Play("AlphaFade", -1, 0f);
    }

    public void ShowHit(Vector3 impactWorldPos)
    {
        Vector3 impactLocalPos = this.transform.root.InverseTransformPoint(impactWorldPos);

        if(impactLocalPos.x < xL)//left
        {
            if(impactLocalPos.z < zB) //lower
            {
                hitIndex = 5;
            }
            else if (impactLocalPos.z > zT) //upper
            {
                hitIndex = 7;
            }
            else //middle
            {
                hitIndex = 6;
            }
        }
        else if (impactLocalPos.x > xR) //right
        {
            if (impactLocalPos.z < zB) //lower
            {
                hitIndex = 3;
            }
            else if (impactLocalPos.z > zT) //upper
            {
                hitIndex = 1;
            }
            else //middle
            {
                hitIndex = 2;
            }
        }
        else //middle
        {
            if (impactLocalPos.z < zB) //lower
            {
                hitIndex = 4;
            }
            else if (impactLocalPos.z > zT) //upper
            {
                hitIndex = 0;
            }
            else //middle
            {
                hitIndex = -1;
            }
        }


        if(hitIndex >= 0 && hitIndex <= 7)
        {
            damageArrows[hitIndex].GetComponent<Animator>().Play("AlphaFade",-1,0f);
        }
    }
}
