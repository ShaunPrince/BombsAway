using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource menuMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuMusic = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //Debug.Log(currentScene.name);
        if (currentScene.name != "MainMenu" && currentScene.name != "MissionMenu")
        {
            Destroy(this.gameObject);
        }
    }
    /*
    private void PlayMusic()
    {
        menuMusic.Play();
    }

    private void StopMusic()
    {
        menuMusic.Stop();
    }
    */
}
