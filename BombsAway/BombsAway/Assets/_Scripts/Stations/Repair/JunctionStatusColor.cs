using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionStatusColor : MonoBehaviour
{
    public Junction j;
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
        if(j.isConnectedToSource)
        {
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[0];
            }
        }
        else
        {
            foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material = materials[1];
            }
        }
    }

}
