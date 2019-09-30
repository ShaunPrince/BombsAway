using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawImageAutoScaleWithCanvas : MonoBehaviour
{
    public RectTransform canvasRect;
    // Start is called before the first frame update
    void Start()
    {
        canvasRect = this.transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        //this.GetComponent<RectTransform>().rect.Set(canvasRect.rect.x,canvasRect.rect.y,canvasRect.rect.width,canvasRect.rect.height);

    }
}
