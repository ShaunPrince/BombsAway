using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{
    public static ControlScheme currentlyActiveControlScheme;
    public bool isActiveControlScheme;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetThisAsActiveControlScheme()
    {
        currentlyActiveControlScheme.isActiveControlScheme = false;
        this.isActiveControlScheme = true;
        currentlyActiveControlScheme = this;
    }
}
