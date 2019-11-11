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
            Vector2 tempNewCords = rectTrans[(int)newStationID].gameObject.GetComponent<DisplayBoxCords>().cords;
            Vector2 tempSchematicCords = schematicRT.gameObject.GetComponent<DisplayBoxCords>().cords;

            centerRT.GetComponent<DisplayBoxCords>().cords = tempSchematicCords;
            schematicRT.gameObject.GetComponent<DisplayBoxCords>().cords = tempNewCords;
            rectTrans[(int)newStationID].gameObject.GetComponent<DisplayBoxCords>().cords = new Vector2(1, 1);
            centerRT = rectTrans[(int)newStationID];
        }

    }

}
