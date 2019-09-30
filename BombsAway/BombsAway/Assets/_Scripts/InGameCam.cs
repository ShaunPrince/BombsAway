using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCam : MonoBehaviour
{
    public int xCord;
    public int yCord;
    public Canvas canvas;


    
    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponentInChildren<Canvas>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
