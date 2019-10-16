using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectWheel : MonoBehaviour
{
    private StationManager stationManager;

    private GraphicRaycaster ray;
    private PointerEventData PointerEventData;
    private EventSystem EventSystem;

    public GameObject wheel;
    public GameObject pointer;
    public GameObject element;

    // Start is called before the first frame update
    void Start()
    {
        stationManager = GameObject.FindGameObjectWithTag("Stations").GetComponent<StationManager>();
        ray = GetComponent<GraphicRaycaster>();
        EventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Tab)) {
            wheel.SetActive(true);
            element.SetActive(true);
            PointerEventData = new PointerEventData(EventSystem);
            PointerEventData.position = Input.mousePosition;

            List<RaycastResult> result = new List<RaycastResult>();

            ray.Raycast(PointerEventData, result);
            if (result.ToArray().Length != 0)
            {
                //Debug.Log(result[0]);
                var pos = result[0].screenPosition;
                if (result[0].gameObject.tag != "Finish")
                {
                    pointer.SetActive(true);
                    float angle = -1 * Mathf.Atan2(pos.x - Screen.width / 2, pos.y - Screen.height / 2) * Mathf.Rad2Deg;
                    //Debug.Log(angle);
                    wheel.transform.rotation = Quaternion.Euler(0, 0, angle);
                    if(result[0].gameObject.GetComponent<SelectionArea>() != null)
                    {
                        stationManager.SetMainStation(result[0].gameObject.GetComponent<SelectionArea>().referencesStation);
                    }
                }
                else
                {
                    pointer.SetActive(false);
                }

            }


        }
        else
            if (wheel.activeSelf) { wheel.SetActive(false); element.SetActive(false); }


    }
}
