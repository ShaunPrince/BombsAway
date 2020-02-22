﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : MonoBehaviour
{
    private float prevRotation;
    private float updateTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialRotation(float rotation)
    {
        prevRotation = rotation;
    }

    public void SmoothRotate(float rotation)
    {
        // if in the middle of a rotation, increase the time
        //if (Mathf.Abs(prevRotation-rotation) > 90) 
        iTween.ValueTo(this.gameObject, iTween.Hash(
                            "from", prevRotation, "to", rotation,
                            "time", updateTime, "easetype", "linear",
                            "onupdate", "Rotate"));
    }

    private void Rotate(float amount)
    {
        this.gameObject.GetComponent<Rigidbody>().MoveRotation(
                    Quaternion.Euler(0, 0, amount));
        prevRotation = this.gameObject.GetComponent<Rigidbody>().rotation.eulerAngles.z;
    } 
}