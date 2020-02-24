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
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
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
        Debug.Log(Mathf.Log10(sliderValue));
        if (sliderValue > 0)
        {
            mixer.SetFloat("MasterSound", Mathf.Log10(sliderValue) * 60);
        }
        else
        {
            mixer.SetFloat("MasterSound", -80.0f);
        }
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
