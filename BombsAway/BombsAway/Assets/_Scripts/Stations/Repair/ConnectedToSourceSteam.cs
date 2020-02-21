using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedToSourceSteam : MonoBehaviour
{
    private Junction j;
    public bool connectedToHealthTank;
    public SourceConnectedColor sourceConnection;
    //public MaterialTweening tweener;
    public float maxSteam;
    public bool currentlyConnected = false;

    private void Start()
    {
        j = this.GetComponent<Junction>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<RepairShaderTween>())
            {
                child.GetComponent<RepairShaderTween>().SetDensityAndSteam(maxSteam, 0f);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
            if (j.isConnectedToSource && !currentlyConnected)
            {
                //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
                currentlyConnected = true;
                TweenAllJunctions(currentlyConnected);
            }
            else if (!j.isConnectedToSource && currentlyConnected)
            {
                //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
                currentlyConnected = false;
                TweenAllJunctions(currentlyConnected);
            }*/
        Debug.Log($"{this.gameObject.name} {j.isConnectedToSource}, {sourceConnection.AtLeastOneTankConnected()}, {currentlyConnected}");
            if (j.isConnectedToSource && sourceConnection.AtLeastOneTankConnected() && !currentlyConnected)
            {
                //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
                currentlyConnected = false;
                TweenAllJunctions(currentlyConnected);
            }
            else if (!j.isConnectedToSource && currentlyConnected)
            {
                //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
                currentlyConnected = true;
                TweenAllJunctions(currentlyConnected);
            }
    }

    private void TweenAllJunctions(bool addSteam)
    {
        if (addSteam)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<RepairShaderTween>())
                    child.GetComponent<RepairShaderTween>().AddSteam();
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<RepairShaderTween>())
                    child.GetComponent<RepairShaderTween>().ReleaseSteam();
            }
        }

    }
}
