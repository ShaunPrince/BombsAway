using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceJunction : MonoBehaviour
{
    public HashSet<Junction> allJunctions;
    public HashSet<Junction> connectedJunctions;
    
    // Start is called before the first frame update
    void Awake()
    {
        allJunctions = new HashSet<Junction>();
        connectedJunctions = new HashSet<Junction>();
        FindAllJunctions();
    }

    // Update is called once per frame
    void Update()
    {
        UnsetAllJunctions();
        ConnectToSource();
        //Debug.Log(connectedJunctions.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        Junction juncToDisconnect = other.gameObject.GetComponentInParent<Junction>();
        if (juncToDisconnect != null && connectedJunctions.Contains(juncToDisconnect))
        {
            connectedJunctions.Remove(juncToDisconnect);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Junction newConnected = other.gameObject.GetComponentInParent<Junction>();
        if (newConnected != null && !connectedJunctions.Contains(newConnected))
        {
            connectedJunctions.Add(newConnected);
        }
    }



    private void FindAllJunctions()
    {
        foreach (Junction j in GameObject.FindObjectsOfType<Junction>())
        {
            allJunctions.Add(j);
        }
    }

    private void UnsetAllJunctions()
    {
        foreach (Junction j in allJunctions)
        {
            j.isConnectedToSource = false;
        }
    }

    private void ConnectToSource()
    {
        foreach (Junction j in connectedJunctions)
        {
            j.SetAndForwardSourceConnection();
        }
    }


}
