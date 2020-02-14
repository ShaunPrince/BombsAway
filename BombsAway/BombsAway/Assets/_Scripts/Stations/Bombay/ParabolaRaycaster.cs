using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaRaycaster : MonoBehaviour
{
    private Transform startingTF;
    private Transform prevTF;
    private Transform nextTF;
    // Start is called before the first frame update
    void Start()
    {
        startingTF = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FollowParabola();
    }

    private void FollowParabola()
    {
        for (int i = 0; i < this.transform.childCount - 1; ++i)
        {
            
        }
    }
}
