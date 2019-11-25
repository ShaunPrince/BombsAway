using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public bool isConnectedToSource;
    public HashSet<Junction> connectedTo;
    public float rotationSpeed;
    public float desiredRotation;
    public float currentRotation;
    // Start is called before the first frame update
    void Awake()
    {
        connectedTo = new HashSet<Junction>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Junction j in connectedTo)
        {
            //Debug.Log(this.gameObject.name + " connects to " + j.gameObject.name);
        }
        currentRotation = this.transform.localRotation.eulerAngles.z;


    }

    private void OnTriggerEnter(Collider other)
    {
        Junction newConnected = other.gameObject.GetComponentInParent<Junction>();
        if (newConnected != null)
        {
            connectedTo.Add(newConnected);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Junction juncToDisconnect = other.gameObject.GetComponentInParent<Junction>();
        if (juncToDisconnect != null && connectedTo.Contains(juncToDisconnect))
        {
            connectedTo.Remove(juncToDisconnect);
        }
    }

    public void SetAndForwardSourceConnection()
    {
        this.isConnectedToSource = true;
        foreach (Junction j in connectedTo)
        {
            if(j.isConnectedToSource == false)
            {
                j.SetAndForwardSourceConnection();
            }
        }
    }



    private void FixedUpdate()
    {
        //currentRotation = Flying.ConvertToPos360Dir(this.transform.rotation.eulerAngles.z);
        //Rotate();
    }


    
    public void RotateCCW()
    {
        this.gameObject.GetComponent<Rigidbody>().
            MoveRotation(Quaternion.Euler(
                0, 0, (currentRotation + 90) % 360));//Mathf.Sign(desiredRotation - currentRotation)
                    //*  this.transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime));
    }

    public void RotateCW()
    {
        this.gameObject.GetComponent<Rigidbody>().
            MoveRotation(Quaternion.Euler(
                0, 0, (currentRotation - 90) % 360 ));//Mathf.Sign(desiredRotation - currentRotation)
                                             //*  this.transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime));
    }
}
