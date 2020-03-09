using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceEdgeConnector : MonoBehaviour
{
    private Junction j;
    //public bool connectedToHealthTank;
    //public SourceConnectedColor sourceConnection;
    //public MaterialTweening tweener;
    public SourceJunction source;
    public float maxSteam;
    public bool currentlyConnected = false;
    public PipeRotations allPipes;
    private bool steamAdded = false;

    private void Start()
    {
        j = this.GetComponent<Junction>();
        //allPipes = this.GetComponentInParent<PipeRotations>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<RepairShaderTween>())
            {
                child.GetComponent<RepairShaderTween>().SetDensityAndSteam(maxSteam, 0f);
            }
        }

    }

    // Update is called once per frame
    void Update()
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
        //Debug.Log($"{this.gameObject.name} {j.isConnectedToSource}, {sourceConnection.AtLeastOneTankConnected()}, {currentlyConnected}");
        if (source.IsConnectedToAtLeastOneJunction() && !currentlyConnected)
        {
            //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
            currentlyConnected = true;
            steamAdded = false;
        }
        else if (source.IsConnectedToAtLeastOneJunction() && currentlyConnected)
        {
            AddSteam();
        }
        else if (!source.IsConnectedToAtLeastOneJunction() && currentlyConnected)
        {
            //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
            currentlyConnected = false;
            steamAdded = false;
            ReleaseSteam();

        }
        else if (!source.IsConnectedToAtLeastOneJunction() && !currentlyConnected)
        {
            ShutOffDensity();
        }
    }

    private void AddSteam()
    {
        // no piece can be rotating
        if (!allPipes.IsAnyPipeRotating() && !steamAdded)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<RepairShaderTween>())
                    child.GetComponent<RepairShaderTween>().AddSteam();
            }
            steamAdded = true;
        }
    }

    private void ReleaseSteam()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<RepairShaderTween>())
                child.GetComponent<RepairShaderTween>().ReleaseSteam();
        }
    }

    private void ShutOffDensity()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<RepairShaderTween>())
            {
                if (child.GetComponent<RepairShaderTween>().SteamTurnedOff())
                {
                    child.GetComponent<RepairShaderTween>().TurnOffDensity();
                }
            }
        }
    }
}
