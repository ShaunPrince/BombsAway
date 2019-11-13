using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectWheel : MonoBehaviour
{
    private bool menuOpen;
    private StationManager stationManager;

    private GraphicRaycaster ray;
    private PointerEventData PointerEventData;
    private EventSystem EventSystem;

    [Header("Hold Tab, move mouse over new camera, let go of tab")]
    public GameObject wheel;
    public GameObject pointer;
    public GameObject element;
    public GameObject center;
    public Sprite[] center_cog;
    private EStationID selectedStation;
    private Station curStation;
    private ECenterCamScale curCamScale;
    private CamZoomControls camZoomCont;
    private StationDisplayPosAndScaleController stationDisplayPosAndScale;

    private List<GameObject> elem;

    private CursorLockMode cursorLockState;
    // Start is called before the first frame update
    void Start()
    {
        menuOpen = false;
        stationManager = GameObject.FindGameObjectWithTag("Stations").GetComponent<StationManager>();
        ray = GetComponent<GraphicRaycaster>();
        EventSystem = GetComponent<EventSystem>();
        camZoomCont = GameObject.FindObjectOfType<CamZoomControls>();
        stationDisplayPosAndScale = GameObject.FindObjectOfType<StationDisplayPosAndScaleController>();

        //the 8 UI sprite slices are here
        elem = new List<GameObject>();
        for (int i = 0; i < 8; i++)
            elem.Add(element.transform.GetChild(i).gameObject);
        foreach (var e in elem)
        {
            e.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f; // make raycast ignore transparency
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuOpen = true;
            cursorLockState = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            curStation = StationManager.currentCenterStation;
            curCamScale = stationDisplayPosAndScale.chosenCenterCamScale;
            camZoomCont.UpdateZoom((int)ECenterCamScale.Med);
        
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            menuOpen = false;
            Cursor.lockState = cursorLockState;
            stationManager.SetMainStation(selectedStation);
            SetUIActive(false);
            camZoomCont.UpdateZoom((int)curCamScale);
        }

        if (menuOpen) 
        {

            SetUIActive(true);
            PointerEventData = new PointerEventData(EventSystem);
            PointerEventData.position = Input.mousePosition;

            foreach (var e in elem)
            {
                e.GetComponent<Image>().enabled = true; //make sure all slices are visible first
            }

            List<RaycastResult> result = new List<RaycastResult>();
            ray.Raycast(PointerEventData, result);

            if (result.ToArray().Length != 0)
            {
                var pos = result[0].screenPosition;

                if (result[0].gameObject.tag != "Finish")
                {
                    pointer.SetActive(true);
                    center.GetComponent<Image>().overrideSprite = center_cog[0]; //revert cog wheel
                    result[0].gameObject.GetComponent<Image>().enabled = false; // "hide" the selected slice

                    float angle = -1 * Mathf.Atan2(pos.x - Screen.width / 2, pos.y - Screen.height / 2) * Mathf.Rad2Deg;

                    wheel.transform.rotation = Quaternion.Euler(0, 0, angle);
                    if(result[0].gameObject.GetComponent<SelectionArea>())
                    {

                        selectedStation = result[0].gameObject.GetComponent<SelectionArea>().referencesStation;
                        //stationManager.SetMainStation(result[0].gameObject.GetComponent<SelectionArea>().referencesStation);
                        //SetUIActive(false);
                    }
                }
                else
                {
                    selectedStation = curStation.stationID;
                    pointer.SetActive(false);
                    center.GetComponent<Image>().overrideSprite = center_cog[1]; //fake selection outline
                }

            }

        }
        else if (wheel.activeSelf) 
        {
            SetUIActive(false);
        }



    }

    private void SetUIActive(bool newState)
    {
        wheel.SetActive(newState);
        element.SetActive(newState);
    }
}
