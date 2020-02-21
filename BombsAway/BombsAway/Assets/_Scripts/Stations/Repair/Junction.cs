using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public bool isConnectedToSource;
    public HashSet<Junction> connectedTo;
    public float rotationSpeed;
    public float desiredRotation;
    //public float currentRotation;
    private int rotationIndex;
    private float[] cwRotationsArray = { 360f, 90f, 180f, 270f };
    private float[] ccwRotationsArray = { 0f, 90f, 180f, 270f };

    private bool clockwise = true;

    // Start is called before the first frame update
    void Awake()
    {
        connectedTo = new HashSet<Junction>();

        for (int i = 0; i < cwRotationsArray.Length; i++)
        {
            if (Mathf.Approximately(this.transform.localRotation.eulerAngles.z, cwRotationsArray[i])) {
                rotationIndex = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Junction j in connectedTo)
        {
            //Debug.Log(this.gameObject.name + " connects to " + j.gameObject.name);
        }
        //currentRotation = this.transform.localRotation.eulerAngles.z;


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
        /*
        this.gameObject.GetComponent<Rigidbody>().
            MoveRotation(Quaternion.Euler(
                0, 0, (currentRotation + 90) % 360));//Mathf.Sign(desiredRotation - currentRotation)
        */                                          //*  this.transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime));

        SetRotationIndex(!clockwise);
        // lerp the rotation
        this.GetComponent<TweenRotation>().SmoothRotate(ccwRotationsArray[rotationIndex]);
    }

    public void RotateCW()
    {
        /*
        this.gameObject.GetComponent<Rigidbody>().
            MoveRotation(Quaternion.Euler(
                0, 0, (currentRotation - 90) % 360 ));//Mathf.Sign(desiredRotation - currentRotation)
        */                                           //*  this.transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime));

        SetRotationIndex(clockwise);
        // lerp the rotation
        this.GetComponent<TweenRotation>().SmoothRotate(cwRotationsArray[rotationIndex]);
    }

    private void SetRotationIndex(bool clockwise)
    {
        if (clockwise)
        {
            if (rotationIndex + 1 > cwRotationsArray.Length-1)
            {
                rotationIndex = 0;
            }
            else
            {
                rotationIndex += 1;
            }
        }
        else
        {
            if (rotationIndex - 1 < 0)
            {
                rotationIndex = cwRotationsArray.Length - 1;
            }
            else
            {
                rotationIndex -= 1;
            }
        }
    }
}
