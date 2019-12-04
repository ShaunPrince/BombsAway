using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMainOverlay : MonoBehaviour
{
    public GameObject manager;
    private int value;

    public Sprite[] overlays;
    private Image self;

    void Awake()
    {
        self = this.GetComponent<Image>();
        //value = (int)manager.GetComponent<StationDisplayPosAndScaleController>().chosenCenterCamScale;
    }



    void LateUpdate() {
        // was gonna have events here but w/e
        value = (int)manager.GetComponent<StationDisplayPosAndScaleController>().chosenCenterCamScale;
        UpdateOverlay(value);

    }
    void UpdateOverlay(int value) {
        self.sprite = overlays[value];
    }
}
