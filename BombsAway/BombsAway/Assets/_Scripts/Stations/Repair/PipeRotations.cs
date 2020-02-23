using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeRotations : MonoBehaviour
{
    public bool IsAnyPipeRotating()
    {
        foreach (Transform child in transform)
        {
            if (child.GetChild(1).GetComponentInChildren<TweenRotation>().IsRotating()) return true;
        }
        return false;
    }
}
