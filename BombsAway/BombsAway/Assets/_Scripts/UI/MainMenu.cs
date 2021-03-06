﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Attach Controls page related object here")]
    public GameObject ControlsPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int index)
    {
        //ShortClank.Play();

        Debug.Log("Menu Start button launches next scene.");
        SceneManager.LoadScene(index);
    }

    public void ViewControls()
    {
        //ShortClank.Play();

        if (ControlsPage.activeSelf) ControlsPage.SetActive(false);
        else ControlsPage.SetActive(true);

    }

    public void Quit()
    {
        //ShortClank.Play();

        Application.Quit();
        Debug.Log("Game is supposed to quit.");
    }

}
