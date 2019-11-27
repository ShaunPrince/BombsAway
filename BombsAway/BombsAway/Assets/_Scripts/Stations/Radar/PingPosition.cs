using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPosition : MonoBehaviour
{
    public float height = 1080;
    public float width = 1920;
    public RectTransform pngRT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionPNG(float worldDistance , float beamDistance)
    {
        Debug.Log(worldDistance * height * .00925);
        pngRT.Translate(0, (worldDistance) * height/2 * .00925f, 0, Space.Self);
    }

    public void RotatePNGParent(float rotationOffset)
    {
        this.GetComponent<RectTransform>().Rotate(0, 0, -rotationOffset, Space.Self);
    }
}
