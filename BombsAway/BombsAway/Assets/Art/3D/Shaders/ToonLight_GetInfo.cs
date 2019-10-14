using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonLight_GetInfo : MonoBehaviour
{
    [SerializeField]
    private Material[] mat = null;


    void Update()
    {
        foreach (Material m in mat)
            m.SetVector("_ToonLightDir", -this.transform.forward);
    }
}
