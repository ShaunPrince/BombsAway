using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookSensitivityController : MonoBehaviour
{
    public Slider slider;
    public GameObject leftGunStationControls;
    public GameObject rightGunStationControls;
    public GameObject tailGunStationControls;

    private float mouseSens;
    private GunnerControls lControls;
    private GunnerControls rControls;
    private GunnerControls tControls;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MouseSensitivity", 0.2f);
        mouseSens = 1.0f;
        lControls = leftGunStationControls.GetComponent<GunnerControls>();
        rControls = rightGunStationControls.GetComponent<GunnerControls>();
        tControls = tailGunStationControls.GetComponent<GunnerControls>();
        SetLookSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLookSensitivity()
    {
        //Debug.Log(slider.value);
        if (lControls == null)
        {
            return;
        }
        float sliderVal = slider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", sliderVal);
        mouseSens = GetMouseSensFromSlider(sliderVal);
        lControls.lookSensitivity = mouseSens;
        rControls.lookSensitivity = mouseSens;
        tControls.lookSensitivity = mouseSens;
    }

    private float GetMouseSensFromSlider(float sliderVal)
    {
        if (sliderVal > 0)
            return sliderVal * 5;
        else
            return 0.01f;
    }
}
