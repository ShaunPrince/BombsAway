using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider fxSlider;
    public Slider musicSlider;

    private AudioManager audioManager;
    private float currentFXSliderVal;
    private float currentMusicSliderVal;

    private void Start()
    {
        if (fxSlider != null)
        {
            fxSlider.value = PlayerPrefs.GetFloat("SoundFXVolume", 0.1f);
            currentFXSliderVal = fxSlider.value;
        }
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
            currentMusicSliderVal = musicSlider.value;
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && fxSlider != null && musicSlider != null)
        {
            if (currentFXSliderVal != fxSlider.value)
            {
                audioManager.PlayShortClankFX();
                currentFXSliderVal = fxSlider.value;
            }
            if (currentMusicSliderVal != musicSlider.value)
            {
                audioManager.PlayShortClankMusic();
                currentMusicSliderVal = musicSlider.value;
            }
        }
    }

    public void SetFXVol()
    {
        if (fxSlider == null)
            return;
        float sliderValue = fxSlider.value;
        if (sliderValue > 0)
        {
            mixer.SetFloat("SoundFXMixer", Mathf.Log(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat("SoundFXMixer", Mathf.Log(0.001f) * 20);
        }
        PlayerPrefs.SetFloat("SoundFXVolume", sliderValue);
    }
    
    public void SetMusicVol()
    {
        if (musicSlider == null)
            return;
        float sliderValue = musicSlider.value;
        if (sliderValue > 0)
        {
            mixer.SetFloat("MusicSound", Mathf.Log(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat("MusicSound", Mathf.Log(0.001f) * 20);
        }
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
