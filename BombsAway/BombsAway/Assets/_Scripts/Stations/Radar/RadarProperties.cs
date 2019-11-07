using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarProperties : MonoBehaviour
{
    public float radius;

    [SerializeField]
    private Transform radarBeamModelTransform;

    // Start is called before the first frame update
    void Awake()
    {
        radarBeamModelTransform.localScale = new Vector3(2, radius, radius);
        radarBeamModelTransform.Translate(0, 0, radius / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
