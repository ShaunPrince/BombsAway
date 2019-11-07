using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationDisplayPosAndScaleController : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform canvasRT;
    private float canvasWidth;
    private float canvasHeight;

    private StationDisplayManager sdm;
    public ECenterCamScale chosenCenterCamScale;
    private float chosenCenterCamScaleValue;

    //Full = 0 , Large = 1, Even = 2
    public List<float> scalePresets;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = this.GetComponentInChildren<Canvas>();
        canvasRT = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasRT.rect.width;
        canvasHeight = canvasRT.rect.height;
        sdm = this.gameObject.GetComponent<StationDisplayManager>();

        chosenCenterCamScaleValue = scalePresets[(int)chosenCenterCamScale];

        ApplyChosenScale();

        PositionRectTrans();

    }
    // Update is called once per frame
    void Update()
    {
        canvasWidth = canvasRT.rect.width;
        canvasHeight = canvasRT.rect.height;
        chosenCenterCamScaleValue = scalePresets[(int)chosenCenterCamScale];

        ApplyChosenScale();

        PositionRectTrans();

    }

    public void PositionRectTrans()
    {

        float stepX = canvasWidth * (1 - chosenCenterCamScaleValue) / 2;
        float stepY = canvasHeight * (1 - chosenCenterCamScaleValue) / 2;


        foreach (RectTransform rt in sdm.rectTrans)
        {
            if(rt == sdm.centerRT)
            {
                rt.localPosition = new Vector3(
                    stepX,
                    stepY,
                    1);
            }
            else
            {
                Vector2 tempCords = rt.GetComponent<DisplayBoxCords>().cords;
                rt.localPosition = new Vector3(
                    (tempCords.x * canvasWidth / 2) - (tempCords.x * stepX / 2),
                    (tempCords.y * canvasHeight / 2) - (tempCords.y * stepY / 2),
                    2);
            }
        }
    }


    public void ApplyChosenScale()
    {
        foreach (RectTransform rt in sdm.rectTrans)
        {
            if (rt == sdm.centerRT)
            {
                ScaleRectTran(rt, chosenCenterCamScaleValue);
            }
            else
            {
                ScaleRectTran(rt, (1 - chosenCenterCamScaleValue) / 2);
            }
        }
    }

    private void ScaleRectTran(RectTransform rt, float newScale)
    {
        rt.sizeDelta = new Vector2(canvasWidth * newScale, canvasHeight * newScale);
    }
}

