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
        if (j.isConnectedToSource && !currentlyConnected)
        {
            //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
            currentlyConnected = true;
            TweenAllJunctions();
                
        }
        else if (!j.isConnectedToSource && currentlyConnected)
        {
            //this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
            currentlyConnected = false;
            TweenAllJunctions();
                
        }
        else if (!j.isConnectedToSource && !currentlyConnected)
        {
            ShutOffDensity();
        }
    }

    private void TweenAllJunctions()
    {
        if (currentlyConnected)
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
