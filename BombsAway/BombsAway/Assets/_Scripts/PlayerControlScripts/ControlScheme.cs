using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScheme : MonoBehaviour
{


    //public string stationTag;

    public bool isActiveControlScheme;

    public virtual void SetActiveControl(bool active)
    {
        isActiveControlScheme = active;
        this.enabled = active;
    }


}
