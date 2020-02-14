using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindImpactCenter : MonoBehaviour
{
    public Vector3 impactCenterPos;

    [SerializeField]
    private Transform parabolaParentTF;

    private Transform startRayTF;
    private Transform nextTf;

    [SerializeField]
    private LayerMask layersToHit;
    // Start is called before the first frame update
    void Start()
    {
        impactCenterPos = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindImpactCent();
    }

    private void FindImpactCent()
    {
        // parabolic Raycast
        for(int i = 0; i < parabolaParentTF.childCount -1; ++i)
        {
            startRayTF = parabolaParentTF.GetChild(i);
            nextTf = parabolaParentTF.GetChild(i+1);
            Vector3 toNextFromStart = nextTf.position - startRayTF.position;
            if (Physics.Raycast(startRayTF.position, nextTf.position - startRayTF.position, out RaycastHit hit
                , Vector3.Magnitude(toNextFromStart), layersToHit))
            {
                impactCenterPos = hit.point;
                return;
            }
        }

    }
}
