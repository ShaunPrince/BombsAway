using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private AudioManager audioManager;
    private float currentSliderVal;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.1f);
        currentSliderVal = slider.value;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Clicked");
            if (currentSliderVal != slider.value)
            {
                audioManager.PlayShortClank();
                currentSliderVal = slider.value;
            }
        }
    }

    public void SetLevel()
    {
        float sliderValue = slider.value;
        if (sliderValue > 0)
        {
            mixer.SetFloat("MasterSound", Mathf.Log(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat("MasterSound", Mathf.Log(0.001f) * 20);
        }
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
