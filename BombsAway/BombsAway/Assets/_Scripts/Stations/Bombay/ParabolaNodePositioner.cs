using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaNodePositioner : MonoBehaviour
{
    private Rigidbody planeRB;



    // Start is called before the first frame update
    void Start()
    {
        planeRB = this.gameObject.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            MoveNode(this.transform.GetChild(i), i);
        }
        
    }

    private void MoveNode(Transform node, float timeSinceDrop)
    {
        float velocityMag = Vector3.Magnitude(Vector3.Scale(planeRB.velocity, new Vector3(1, 0, 1)));
        node.localPosition = new Vector3(0, Physics.gravity.y * .5f * timeSinceDrop * timeSinceDrop, velocityMag * timeSinceDrop);
    }
}
