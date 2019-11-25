using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStatusColor : MonoBehaviour
{
    public TankController tc;
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {

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
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[2];
            }
        }
    }
}
