using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{


    //public string stationTag;

    public bool isActiveControlScheme;

    public virtual void SetAsActiveControl()
    {
        isActiveControlScheme = true;
        this.enabled = true;
    }

    public virtual void SetAsInactive()
    {
        isActiveControlScheme = false;
        this.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
