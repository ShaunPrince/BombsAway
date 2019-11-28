using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCamTrack : MonoBehaviour
{
    public Transform impactSpotTF;
    public LayerMask layersToHit;

    private Vector3 centerOfImpactAreaPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(impactSpotTF.position,Vector3.down, out hit, 10000f,layersToHit))
        {
            centerOfImpactAreaPos = hit.point;
        }
        Debug.DrawLine(this.transform.position , centerOfImpactAreaPos);
        this.gameObject.transform.LookAt(centerOfImpactAreaPos);
    }
}
