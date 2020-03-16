using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationDisplayManager : MonoBehaviour
{
    public List<RectTransform> rectTrans;
    public RectTransform centerRT;
    [SerializeField]
    private RectTransform schematicRT;
    
    private StationDisplayPosAndScaleController posAndScaleCont;
    // Start is called before the first frame update
    void Awake()
    {
        //InitializeRawImageList();

    }

    public void SetMainStation(EStationID newStationID)
    {

        SwapCenterRectTranCords(newStationID);
    }

    public void SwapCenterRectTranCords(EStationID newStationID)
    {
        if(centerRT != rectTrans[(int)newStationID])
        {
            float fadeTime = 1f;
            Vector2 tempNewCords = rectTrans[(int)newStationID].gameObject.GetComponent<DisplayBoxCords>().cords;
            Vector2 tempSchematicCords = schematicRT.gameObject.GetComponent<DisplayBoxCords>().cords;
            RectTransform tempOldRectTransform = centerRT;

            // Camera fade out
            centerRT.GetChild(0).GetComponent<CameraTween>().FadeOut(fadeTime);
            schematicRT.GetChild(0).GetComponent<CameraTween>().FadeOut(fadeTime);
            rectTrans[(int)newStationID].GetChild(0).GetComponent<CameraTween>().FadeOut(fadeTime);

            centerRT.GetComponent<DisplayBoxCords>().cords = tempSchematicCords;
            schematicRT.gameObject.GetComponent<DisplayBoxCords>().cords = tempNewCords;
            rectTrans[(int)newStationID].gameObject.GetComponent<DisplayBoxCords>().cords = new Vector2(1, 1);
            centerRT = rectTrans[(int)newStationID];

            // Camera fade in
            tempOldRectTransform.GetChild(0).GetComponent<CameraTween>().FadeIn(fadeTime);
            schematicRT.GetChild(0).GetComponent<CameraTween>().FadeIn(fadeTime);
            //rectTrans[(int)newStationID].GetChild(0).GetComponent<CameraTween>().FadeIn();
            centerRT.GetChild(0).GetComponent<CameraTween>().FadeIn(fadeTime);
        }

    }

}
