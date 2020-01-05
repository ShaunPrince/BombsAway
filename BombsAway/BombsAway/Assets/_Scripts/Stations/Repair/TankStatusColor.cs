using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStatusColor : MonoBehaviour
{
    public TankController tc;
    public Material[] materials;

    public float timeBetweenFlickers;
    private float timeSinceLastFlicker;

    private int flickerIndex;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastFlicker = 0f;
        flickerIndex = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColorIfNeeded();
    }

    private void ChangeColorIfNeeded()
    {
        if (tc.currentFillLevel == tc.maxFillLevel)
        {
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[0];
            }
        }
        else if (tc.currentFillLevel < tc.maxFillLevel && tc.isConnectedToSource)
        {
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[1];
            }
        }
        else if (tc.currentFillLevel < tc.maxFillLevel && !tc.isConnectedToSource)
        {
            timeSinceLastFlicker += Time.deltaTime;
            if(timeSinceLastFlicker >= timeBetweenFlickers)
            {
                if(flickerIndex == 2)
                {
                    flickerIndex = 3;
                }
                else
                {
                    flickerIndex = 2;
                }
                timeSinceLastFlicker = 0;
            }
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[flickerIndex];
            }
        }
    }
}
