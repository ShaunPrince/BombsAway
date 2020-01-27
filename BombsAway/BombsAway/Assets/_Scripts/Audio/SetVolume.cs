using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
    }
    public void SetLevel()
    {
        float sliderValue = slider.value;
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
